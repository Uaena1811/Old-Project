using Kimerce.Backend.Domain.News;
using Kimerce.Backend.Dto;
using Kimerce.Backend.Dto.Items.News;
using Kimerce.Backend.Dto.Results;
using Kimerce.Backend.Infrastructure.Repositories;
using Kimerce.Backend.Infrastructure.SmartTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kimerce.Backend.Infrastructure.Services.Categories
{
    public interface INewsService
    {
        Task<SmartTableResult<NewsModel>> Search(SmartTableParam param);
        Task<BaseResult> CreateOrUpdate(NewsModel model, int updateBy = 0, string updateByUserName = "");
        Task<BaseResult> Delete(int newsId);
        News GetById(int id);
    }
    public class NewsService : INewsService
    {
        private readonly IRepository<News> _newsRepository;
        public NewsService(IRepository<News> newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public IQueryable<News> Index()
        {

            return _newsRepository.Query().Where(c => c.IsDeleted == false);
        }
        public News GetById(int id)
        {
            if (_newsRepository.Query().Where(c => c.Id == id).Any(e => e.IsDeleted == false))
            { return _newsRepository.GetById(id); }

            return null;
        }
        public async Task<SmartTableResult<NewsModel>> Search(SmartTableParam param)
        {
            var query = _newsRepository.Query();
            if (param.Search.PredicateObject != null)
            {
                dynamic search = param.Search.PredicateObject;
                if (search.Keyword != null)
                {
                    string keyword = search.Keyword;
                    keyword = keyword.Trim().ToLower();
                    query = query.Where(x => x.Title.Contains(keyword));
                }

                if (search.CreatedStart != null && search.CreatedEnd != null)
                {
                    DateTimeOffset start = search.CreatedStart;
                    DateTimeOffset end = search.CreatedEnd;
                    query = query.Where(x => (x.CreatedTime >= start) && (x.CreatedTime <= end));
                }
            }
            param.Sort = new Sort() { Predicate = "DisplayOder", Reverse = false };
            var gridData = query.ToSmartTableResult(param, x => x.ToModel());
            return gridData;
        }

        private async Task<BaseResult> Update(News news, int updateBy = 0, string updateByUserName = "")
        {
            var result = new BaseResult() { Result = Result.Success };
            var newsForUpdate = _newsRepository.Query().FirstOrDefault(p => p.Id == news.Id);
            if (newsForUpdate == null || news.Id <= 0)
            {
                result.Result = Result.Failed;
                result.Message = "Không tìm thấy blog yêu cầu!";
                return result;
            }
            else
            {
                newsForUpdate.Title = news.Title.Trim();
                newsForUpdate.ShortDescription = news.ShortDescription;
                newsForUpdate.Description = news.Description;
                newsForUpdate.Status = news.Status;

            }
            try
            {
                newsForUpdate = news.ToNews(newsForUpdate);
                //Cập nhật thông tin chung của thực thể
                newsForUpdate = newsForUpdate.UpdateCommonInt(updateBy, updateByUserName);

                await _newsRepository.UpdateAsync(newsForUpdate);
            }
            catch (Exception e)
            {
                result.Result = Result.SystemError;
                result.Message = e.ToString();
            }

            return result;
        }
        private async Task<BaseResult> Create(News news)
        {
            var result = new BaseResult();
            news.Title = news.Title.Trim();
            try
            {
                await _newsRepository.InsertAsync(news);
            }
            catch (Exception e)
            {
                result.Result = Result.SystemError;
                result.Message = e.ToString();
            }
            return result;
        }
        public async Task<BaseResult> CreateOrUpdate(NewsModel model, int updateBy = 0, string updateByUserName = "")
        {

            var news = model.ToNews();
            //Cập nhật thông tin chung của thực thể
            news = news.UpdateCommonInt(updateBy, updateByUserName);
            if (news.Id > 0)
            {
                //Cập nhật
                return await Update(news);
            }
            else
            {
                //Thêm mới
                return await Create(news);
            }
        }
        public async Task<BaseResult> Delete(int newsId)
        {
            var rs = new BaseResult() { Result = Result.Success };
            if (newsId > 0)
            {
                var news = _newsRepository.Query().FirstOrDefault(c => c.Id == newsId);
                if (news != null)
                {
                    news.IsDeleted = true;
                    await _newsRepository.UpdateAsync(news);
                    try
                    {
                        await _newsRepository.SaveChangeAsync();
                    }
                    catch (Exception ex)
                    {
                        rs.Result = Result.SystemError;
                        rs.Message = ex.ToString();
                    }
                }
                else
                {
                    rs.Message = "Không tìm thấy blog!";
                    rs.Result = Result.Failed;
                }

            }
            else
            {
                rs.Message = "id không hợp lệ!";
                rs.Result = Result.Failed;
            }
            return rs;
        }
    }
}
