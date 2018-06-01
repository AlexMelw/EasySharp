namespace EasySharp.EasyWeb.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NHelpers.Collections.Extensions;

    public class Paginator
    {
        private readonly int _totalPages;
        private readonly int _currentPageIndex;

        #region CONSTRUCTORS

        private Paginator(int totalPages, int currentPageIndex)
        {
            _totalPages = totalPages;
            _currentPageIndex = currentPageIndex;
        }

        #endregion

        public static Paginator Create(int totalPages, int currentPageIndex)
        {
            return new Paginator(totalPages, currentPageIndex);
        }

        public IEnumerable<PageMetaThumbnail> GetPaginationLayout()
        {
            var pageMetaThumbnailsList = new List<PageMetaThumbnail>();

            for (int pageIndex = 0; pageIndex < _totalPages; pageIndex++)
                pageMetaThumbnailsList.Add(new PageMetaThumbnail
                {
                    Index = pageIndex + 1,
                    IsActive = _currentPageIndex == pageIndex + 1
                });

            var pageMetaThumbnailsWithBreadcrumbswithBreadcrumbs =
                ReplaceExtraPageMetaThumbnailsWithBreadcrumbs(pageMetaThumbnailsList);

            return pageMetaThumbnailsWithBreadcrumbswithBreadcrumbs;
        }

        private IEnumerable<PageMetaThumbnail> ReplaceExtraPageMetaThumbnailsWithBreadcrumbs(
            List<PageMetaThumbnail> pageMetaThumbnailsList)
        {
            #region Advanced Breadcrumbs Creator

            void AdvancedBreadcrumbsCreator(List<PageMetaThumbnail> list,
                Func<PageMetaThumbnail, bool> breadcrumbsDetectorPredicate)
            {
                // Possible breadcrumbs
                var possibleBreadcrumbsThumbnails = list.Where(breadcrumbsDetectorPredicate).ToList();

                var singleBreadcrumbThumbnail = possibleBreadcrumbsThumbnails
                    .Take(1)
                    .ForEachDo(thumbnail => { thumbnail.Index = -1; })
                    .FirstOrDefault();

                possibleBreadcrumbsThumbnails.Remove(singleBreadcrumbThumbnail);

                list.RemoveRange(possibleBreadcrumbsThumbnails);
            }

            void AdvancedLeftBreadcrumbsCreator(List<PageMetaThumbnail> pageMetaThumbnails)
            {
                int maxIndex = pageMetaThumbnails.Max(thumb => thumb.Index);

                AdvancedBreadcrumbsCreator(pageMetaThumbnails, thumbnail =>
                    thumbnail.Index > 2 // Left bound
                    && thumbnail.Index < (_currentPageIndex + 4 >= maxIndex
                        ? maxIndex - 4
                        : _currentPageIndex - 2)); // Right bound
            }

            void AdvancedRightBreadcrumbsCreator(List<PageMetaThumbnail> pageMetaThumbnails)
            {
                int maxIndex = pageMetaThumbnails.Max(thumb => thumb.Index);

                AdvancedBreadcrumbsCreator(pageMetaThumbnails, thumbnail =>
                    thumbnail.Index > (_currentPageIndex <= 5 ? 5 : _currentPageIndex + 2) // Left bound
                    && thumbnail.Index <= maxIndex - 2); // Right bound
            }

            #endregion

            #region Simple Breadcrumbs Creator

            void SimpleBreadcrumbsCreator(List<PageMetaThumbnail> pageMetaThumbnails, int from, int count)
            {
                var possibleBreadcrumbsThumbnails =
                    pageMetaThumbnails.SkipWhile(thumb => thumb.Index < from).Take(count).ToList();

                var singleBreadcrumbThumbnail = possibleBreadcrumbsThumbnails
                    .Take(1)
                    .ForEachDo(thumbnail => { thumbnail.Index = -1; })
                    .FirstOrDefault();

                possibleBreadcrumbsThumbnails.Remove(singleBreadcrumbThumbnail);

                pageMetaThumbnails.RemoveRange(possibleBreadcrumbsThumbnails);
            }

            void SimpleLeftBreadcrumbsCreator(List<PageMetaThumbnail> pageMetaThumbnails)
            {
                int maxIndex = pageMetaThumbnails.Max(thumb => thumb.Index);

                SimpleBreadcrumbsCreator(pageMetaThumbnails,
                    3, maxIndex > 8 ? maxIndex - 7 : 0);
            }

            void SimpleRightBreadcrumbsCreator(List<PageMetaThumbnail> pageMetaThumbnails)
            {
                int maxIndex = pageMetaThumbnails.Max(thumbnail => thumbnail.Index);

                SimpleBreadcrumbsCreator(pageMetaThumbnails,
                    6, maxIndex > 8 ? maxIndex - 7 : 0);
            }

            #endregion

            if (pageMetaThumbnailsList.Max(thumbnail => thumbnail.Index) <= 10)
            {
                if (_currentPageIndex <= 5)
                    SimpleRightBreadcrumbsCreator(pageMetaThumbnailsList);
                else // _currentPageIndex in 6...10
                    SimpleLeftBreadcrumbsCreator(pageMetaThumbnailsList);
            }
            else // pageMetaThumbnailsList.Max(thumbnail => thumbnail.Index) > 10
            {
                AdvancedLeftBreadcrumbsCreator(pageMetaThumbnailsList);
                AdvancedRightBreadcrumbsCreator(pageMetaThumbnailsList);
            }

            return pageMetaThumbnailsList;
        }

        public enum PaginationSize
        {
            Small,
            Normal,
            Large
        }

        public class PageMetaThumbnail
        {
            public int Index { get; set; }
            public bool IsActive { get; set; }
            public bool IsBreadcrumb => Index == -1;
            public string TextValue => IsBreadcrumb ? "..." : Index.ToString();
        }
    }
}