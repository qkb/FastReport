using System.Collections.Generic;

namespace FastReport.Engine
{
    public partial class ReportEngine
    {
        #region Private Classes

        private class PageNumberInfo
        {
            #region Fields

            public int pageNo;
            public int totalPages;

            #endregion Fields
        }

        #endregion Private Classes

        #region Fields

        private List<PageNumberInfo> pageNumbers;
        private int logicalPageNo;

        #endregion Fields

        #region Private Methods

        private void InitPageNumbers()
        {
            pageNumbers = new List<PageNumberInfo>();
            logicalPageNo = 0;
        }

        /// <summary>
        /// Resets the logical page numbers.
        /// </summary>
        public void ResetLogicalPageNumber()
        {
            if (!FirstPass)
                return;

            for (int i = pageNumbers.Count - 1; i >= 0; i--)
            {
                PageNumberInfo info = pageNumbers[i];
                info.totalPages = logicalPageNo;
                if (info.pageNo == 1)
                    break;
            }

            logicalPageNo = 0;
        }

        private int GetLogicalPageNumber()
        {
            int index = CurPage - firstReportPage;
            return pageNumbers[index].pageNo + Report.InitialPageNumber - 1;
        }

        private int GetLogicalTotalPages()
        {
            int index = CurPage - firstReportPage;
            return pageNumbers[index].totalPages + Report.InitialPageNumber - 1;
        }

        #endregion Private Methods

        #region Internal Methods

        internal void IncLogicalPageNumber()
        {
            logicalPageNo++;
            int index = CurPage - firstReportPage;
            if (FirstPass || index >= pageNumbers.Count)
            {
                PageNumberInfo info = new PageNumberInfo();
                pageNumbers.Add(info);
                info.pageNo = logicalPageNo;
            }
        }

        #endregion Internal Methods
    }
}
