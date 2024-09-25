
using System.Reflection;
using System.Text;
using RetailApp.Entities.Models;
using RetailApp.Entities.RequestFeatures;

namespace RetailApp.API.Repository
{
    /// <summary>
    /// This class extends the IQueryable interface to add extension methods, great way to reused filter related methods
    /// </summary>
    public static class RepositoryProductExtensions
    {
        public static IQueryable<Product> Search(this IQueryable<Product> products, string searchTerm)
        {
            //search using Product name
            if (string.IsNullOrWhiteSpace(searchTerm))
                return products;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            
            return products.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTerm));
        }
        public static IQueryable<Product> FilterBy(this IQueryable<Product> products, ProductParameters productParameters) { 
            if(productParameters == null) return products;
            //Price range
            if (productParameters.PriceFrom > 0 & productParameters.PriceTo > 0)
            {
                products = products.Where(p => p.Price >= productParameters.PriceFrom && p.Price <= productParameters.PriceTo);
            }
            // posted date range
            return products;
        }
        public static IQueryable<Product> Sort(this IQueryable<Product> products,
            string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return products.OrderBy(e => e.Name);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Product)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi =>
                pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery))
                return products.OrderBy(e => e.Name);

            return products.OrderBy(orderQuery => orderQuery);
        }
    }
}
