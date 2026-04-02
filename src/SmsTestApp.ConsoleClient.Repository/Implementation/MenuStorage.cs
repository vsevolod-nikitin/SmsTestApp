using Microsoft.EntityFrameworkCore;
using SmsTestApp.ConsoleClient.Repository.Model;
using SmsTestApp.Contracts.Menu;

namespace SmsTestApp.ConsoleClient.Repository.Implementation
{
    /// <summary>
    /// Реализация функционала хранения информации о блюдах.
    /// </summary>
    /// <param name="contextFactory">Фабрика контекстов данных.</param>
    internal sealed class MenuStorage(IDbContextFactory<StorageContext> contextFactory) : IMenuStorage
    {
        private readonly HashSet<string> _articles = [];

        /// <inheritdoc/>
        public async Task UpdateProductsAsync(IEnumerable<MenuItemDto> products)
        {
            ArgumentNullException.ThrowIfNull(products);

            var productList = products.ToList();
            if (productList.Count == 0)
            {
                return;
            }

            await using var context = await contextFactory.CreateDbContextAsync();

            var ids = productList.Select(p => p.Id).Distinct().ToList();

            // Загружаем существующие элементы с их штрихкодами
            var existingItems = await context.MenuItems
                .Include(mi => mi.Barcodes)
                .Where(mi => ids.Contains(mi.Id))
                .ToListAsync();

            // Если в базе данных уже есть элемент с таким Id, то обновляем его поля и штрихкоды, иначе создаём новый элемент
            _articles.Clear();
            foreach (var prod in productList)
            {
                _articles.Add(prod.Article);

                var existing = existingItems.FirstOrDefault(e => e.Id == prod.Id);
                if (existing is not null)
                {
                    // Обновляем поля
                    existing.Article = prod.Article;
                    existing.Name = prod.Name;
                    existing.Price = prod.Price ?? existing.Price;
                    existing.IsWeighted = prod.IsWeighted;
                    existing.FullPath = prod.FullPath;

                    var newBarcodes = new HashSet<string>(prod.Barcodes ?? []);
                    var existingBarcodes = existing.Barcodes.Select(b => b.Value).ToHashSet();

                    // Удаляем штрихкоды, которых больше нет
                    var toRemove = existing.Barcodes.Where(b => !newBarcodes.Contains(b.Value)).ToList();
                    foreach (var rem in toRemove)
                    {
                        context.Barcodes.Remove(rem);
                    }

                    // Добавляем новые штрихкоды
                    var toAdd = newBarcodes.Except(existingBarcodes, StringComparer.Ordinal);
                    foreach (var value in toAdd)
                    {
                        existing.Barcodes.Add(new Barcode { Value = value, MenuItemId = existing.Id });
                    }
                }
                else
                {
                    // Создаём новую запись вместе со штрихкодами
                    var entity = new MenuItem
                    {
                        Id = prod.Id,
                        Article = prod.Article,
                        Name = prod.Name,
                        Price = prod.Price ?? 0m,
                        IsWeighted = prod.IsWeighted,
                        FullPath = prod.FullPath,
                        Barcodes = (prod.Barcodes ?? []).Select(b => new Barcode { Value = b, MenuItemId = prod.Id }).ToList()
                    };

                    await context.MenuItems.AddAsync(entity);
                }
            }

            await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public Task<bool> IsArticleValidAsync(string article)
        {
            // Можно проверить наличие артикула в БД, но как вариант используем кэш (т.к. регистрируемся как Singleton).
            return Task.FromResult(_articles.Contains(article));
        }
    }
}
