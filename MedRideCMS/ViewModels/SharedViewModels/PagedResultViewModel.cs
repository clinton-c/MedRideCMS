using MedRideCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MedRideCMS.ViewModels.SharedViewModels
{
    public class PagedResultViewModel<T>
    {
        private IEnumerable<T> _dataSource;

        public int Page { get; set; }
        public int TotalNumItems { get; set; }
        public int TotalNumPages { get; set; }
        public int PageSize { get; set; }

        public PagedResultViewModel(int currentPage, int pageSize, IEnumerable<T> dataSource)
        {
            this.Page = currentPage;
            this.PageSize = pageSize;
            this._dataSource = dataSource;
            TotalNumItems = dataSource.Count();
            TotalNumPages = (int)Math.Ceiling(decimal.Divide(TotalNumItems, pageSize));
            TotalNumPages = TotalNumPages == 0 ? 1 : TotalNumPages;
        }

        public IEnumerable<T> PageItems
        {
            get
            {
                return _dataSource.Skip(Page * PageSize).Take(PageSize);
            }
        }

        public int[] GetPageNavigationList(int numPagesToDisplay)
        {
            numPagesToDisplay = TotalNumPages >= numPagesToDisplay ? numPagesToDisplay : TotalNumPages;
            var pageList = new int[numPagesToDisplay];
            var index = 0;
            pageList[index] = Page;
            index++;

            var offset = 1;
            while (index < numPagesToDisplay)
            {
                if (Page - offset >= 0)
                {
                    pageList[index] = Page - offset;
                    index++;
                }

                if (Page + offset < TotalNumPages && index < numPagesToDisplay)
                {
                    pageList[index] = Page + offset;
                    index++;
                }

                offset++;
            }

            return pageList.OrderBy(c => c).ToArray();
        }


        public bool IsFirstPage
        {
            get
            {
                return Page == 0 ? true : false;
            }
        }

        public bool IsLastPage
        {
            get
            {
                return Page == TotalNumPages - 1 ? true : false;
            }
        }

        public int PreviousPage
        {
            get
            {
                return IsFirstPage ? FirstPage : Page - 1;
            }
        }

        public int NextPage
        {
            get
            {
                return IsLastPage ? LastPage : Page + 1;
            }
        }

        public int FirstPage
        {
            get { return 0; }
        }

        public int LastPage
        {
            get { return TotalNumPages - 1; }
        }
       
    }
}