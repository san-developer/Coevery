﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Orchard.ContentManagement;
using Orchard.Core.Title.Models;
using Orchard.Localization;
using Orchard.Projections.Models;
using Orchard.Utility.Extensions;

namespace Coevery.Projections.Controllers {
    public class ProjectionController : ApiController {
         private readonly IContentManager _contentManager;

        public ProjectionController(IContentManager contentManager) {
            _contentManager = contentManager;
        }

        public IEnumerable<JObject> Get()
        {
            List<JObject> re = new List<JObject>();
            var projections = _contentManager.Query<ProjectionPart>().List();
            foreach (var projectionPart in projections)
            {
                string displayName = _contentManager.Get(projectionPart.Record.QueryPartRecord.Id).As<TitlePart>().Title;
                JObject reObJ = new JObject();
                reObJ["ContentId"] = projectionPart.Id;
                reObJ["EntityType"] = projectionPart.As<TitlePart>().Title;
                reObJ["DisplayName"] = displayName;
                re.Add(reObJ);
            }
            return re;
        }

        public IEnumerable<JObject> Get(string id)
        {
            var pluralService = PluralizationService.CreateService(new CultureInfo("en-US"));
            if (pluralService.IsPlural(id))
            {
                id = pluralService.Singularize(id);
            }

            List<JObject> re = new List<JObject>();
            var projections = _contentManager.Query<ProjectionPart>().List().Where(t => t.As<TitlePart>().Title == id);
            foreach (var projectionPart in projections)
            {
                string displayName = _contentManager.Get(projectionPart.Record.QueryPartRecord.Id).As<TitlePart>().Title;
                JObject reObJ = new JObject();
                reObJ["ContentId"] = projectionPart.Id;
                reObJ["EntityType"] = projectionPart.As<TitlePart>().Title;
                reObJ["DisplayName"] = displayName;
                re.Add(reObJ);
            }
            return re;
        }

        public void Delete(int id)
        {
            var contentItem = _contentManager.Get(id);
            _contentManager.Remove(contentItem);
        }
    }
}