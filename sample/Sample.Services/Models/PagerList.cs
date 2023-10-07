using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Services.Models
{
    public interface IPagerBase
    {
        int Page { get; set; }

        int PageSize { get; set; }

        int TotalCount { get; set; }
    }

    [Serializable]
    public class PagerList<T> : IPagerBase
    {
        public PagerList()
          : this(0)
        {
        }

        public PagerList(IEnumerable<T> data = null)
          : this(0, data)
        {
        }

        public PagerList(int totalCount, IEnumerable<T> data = null)
          : this(1, 20, totalCount, data)
        {
        }

        public PagerList(int page, int pageSize, int totalCount, IEnumerable<T> data = null)
          : this(page, pageSize, totalCount, "", data)
        {
        }

        public PagerList(int page, int pageSize, int totalCount, string order, IEnumerable<T> data = null)
        {
            this.Data = (data != null ? data.ToList<T>() : (List<T>)null) ?? new List<T>();
            Pager pager = new Pager(page, pageSize, totalCount);
            this.TotalCount = pager.TotalCount;
            this.PageCount = pager.GetPageCount();
            this.Page = pager.Page;
            this.PageSize = pager.PageSize;
            this.Order = order;
        }

        public PagerList(IPager pager, IEnumerable<T> data = null)
          : this(pager.Page, pager.PageSize, pager.TotalCount, pager.Order, data)
        {
        }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int PageCount { get; set; }

        public string Order { get; set; }

        public List<T> Data { get; set; }

        public T this[int index]
        {
            get => this.Data[index];
            set => this.Data[index] = value;
        }

        public void Add(T item) => this.Data.Add(item);

        public void AddRange(IEnumerable<T> collection) => this.Data.AddRange(collection);

        public void Clear() => this.Data.Clear();

        public PagerList<TResult> Convert<TResult>(Func<T, TResult> converter) => this.Convert<TResult>(this.Data.Select<T, TResult>(converter));

        public PagerList<TResult> Convert<TResult>(IEnumerable<TResult> data) => new PagerList<TResult>(this.Page, this.PageSize, this.TotalCount, this.Order, data);
    }

    public class Pager : IPager, IPagerBase
    {
        protected int _pageIndex;

        public Pager()
            : this(1)
        {
        }

        public Pager(int page, int pageSize, string order)
            : this(page, pageSize, 0, order)
        {
        }

        public Pager(int page, int pageSize = 20, int totalCount = 0, string order = "")
        {
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
            this.Order = order;
            this.Page = page;
        }

        public virtual int Page
        {
            get => this.GetPageIndex();
            set => this._pageIndex = value;
        }

        public virtual int PageSize { get; set; }

        public virtual int TotalCount { get; set; }

        public int GetPageCount() => this.TotalCount % this.PageSize == 0 ? this.TotalCount / this.PageSize : this.TotalCount / this.PageSize + 1;

        public int GetSkipCount() => this.PageSize * (this.Page - 1);

        public virtual string Order { get; set; }

        public int GetStartNumber() => (this.Page - 1) * this.PageSize + 1;

        public int GetEndNumber() => this.Page * this.PageSize;

        protected int GetPageIndex()
        {
            if (this._pageIndex <= 0)
                this._pageIndex = 1;
            return this._pageIndex;
        }
    }

    public interface IPager : IPagerBase
    {
        int GetPageCount();

        int GetSkipCount();

        string Order { get; set; }

        int GetStartNumber();

        int GetEndNumber();
    }
}
