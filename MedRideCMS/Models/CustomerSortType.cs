using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedRideCMS.Models
{
    public static class CustomerSortType
    {
        public const int DEFAULT = 0;
        public const int FIRSTNAME_ASCENDING = 1;
        public const int FIRSTNAME_DESCENDING = 2;
        public const int LASTNAME_ASCENDING = 3;
        public const int LASTNAME_DESCENDING = 4;
        public const int STATE_ASCENDING = 5;
        public const int STATE_DESCENDING = 6;
        public const int UPDATED_ASCENDING = 7;
        public const int UPDATED_DESCENDING = 8;
        public const int CREATED_ASCENDING = 9;
        public const int CREATED_DESCENDING = 10;



        public static IEnumerable<Customer> SortCustomersBy(IEnumerable<Customer> customers, int sortBy)
        {
            switch (sortBy)
            {
                case CustomerSortType.FIRSTNAME_ASCENDING:
                    customers = customers.OrderBy(c => c.FirstName);
                    break;
                case CustomerSortType.FIRSTNAME_DESCENDING:
                    customers = customers.OrderByDescending(c => c.FirstName);
                    break;
                case CustomerSortType.LASTNAME_ASCENDING:
                    customers = customers.OrderBy(c => c.LastName);
                    break;
                case CustomerSortType.LASTNAME_DESCENDING:
                    customers = customers.OrderByDescending(c => c.LastName);
                    break;
                case CustomerSortType.STATE_ASCENDING:
                    customers = customers.OrderBy(c => c.State.Name);
                    break;
                case CustomerSortType.STATE_DESCENDING:
                    customers = customers.OrderByDescending(c => c.State.Name);
                    break;
                case CustomerSortType.UPDATED_ASCENDING:
                    customers = customers.OrderBy(c => c.Updated);
                    break;
                case CustomerSortType.UPDATED_DESCENDING:
                    customers = customers.OrderByDescending(c => c.Updated);
                    break;
                case CustomerSortType.CREATED_ASCENDING:
                    customers = customers.OrderBy(c => c.Created);
                    break;
                case CustomerSortType.CREATED_DESCENDING:
                    customers = customers.OrderByDescending(c => c.Created);
                    break;
                default:
                    break;
            }

            return customers;
        }

    }
}