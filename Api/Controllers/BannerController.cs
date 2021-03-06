﻿using Domain;
using System.Threading.Tasks;
using System.Web.Http;

namespace Api.Controllers
{
    [RoutePrefix("api/v1/banners")]
    public class BannerController : ApiController
    {
        private readonly IBannerRepository _bannerRepository;
        private readonly IHtmlValidator _htmlValidator;
        private readonly IIdGenerator _idGenerator;

        public BannerController(IBannerRepository bannerRepository, IHtmlValidator htmlValidator, IIdGenerator idGenerator)
        {
            _bannerRepository = bannerRepository;
            _htmlValidator = htmlValidator;
            _idGenerator = idGenerator;
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var banner = _bannerRepository.Find(id);
            if(banner == null)
            {
                return NotFound();
            }

            return Ok(banner);
        }

        [Route("")]
        public async Task<IHttpActionResult> Post(BannerDTO bannerModel)
        {
            var validationResult = await _htmlValidator.ValidateAsync(bannerModel.Html);
            if (validationResult.IsValid)
            {
                var banner = Banner.Create(bannerModel.Html, _idGenerator);
                _bannerRepository.Add(banner);
                return Ok(banner);
            }
            else
            {
                return BadRequest(string.Join("\n", validationResult.ErrorMessages));
            }
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(int id, BannerDTO bannerModel)
        {
            var banner = _bannerRepository.Find(id);
            if(banner == null)
            {
                return NotFound();
            }

            var validationResult = await _htmlValidator.ValidateAsync(bannerModel.Html);
            if(validationResult.IsValid)
            {
                banner.UpdateHtml(bannerModel.Html);
                return Ok(banner);
            }
            else
            {
                return BadRequest(string.Join("\n", validationResult.ErrorMessages));
            }
        }

        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var banner = _bannerRepository.Find(id);
            if(banner == null)
            {
                return NotFound();
            }

            _bannerRepository.Delete(banner);
            return Ok(banner);
        }

        /// <summary>
        /// Returns a banners html as body and content-type set to text/html
        /// </summary>
        /// <param name="id">The banner id</param>
        /// <returns>A html string</returns>
        [HttpGet]
        [Route("~/banners/{id:int}")]
        public IHttpActionResult RenderBannerHtml(int id)
        {
            var banner = _bannerRepository.Find(id);
            if (banner == null)
            {
                return NotFound();
            }

            return Html(banner.Html);
        }

        public IHttpActionResult Html(string html)
        {
            return new HtmlResult(html, Request);
        }
    }
}
