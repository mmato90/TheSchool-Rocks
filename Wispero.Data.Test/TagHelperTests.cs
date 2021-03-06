﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wispero.Entities;

namespace Wispero.Data.Test
{
    [TestClass]
    public class TagHelperTests
    {
        [TestMethod]
        public void DependencyInjected()
        {
            int max;
            Moq.Mock<Wispero.Core.Services.IQueryService<KnowledgeBaseItem>> queryService = new Moq.Mock<Core.Services.IQueryService<KnowledgeBaseItem>>();

            queryService.Setup(x => x.GetAll()).Returns(
              new System.Collections.Generic.List<KnowledgeBaseItem>() {
                    new KnowledgeBaseItem { Id = 1, Query = "Question1", Answer = "Answer1", Tags = "Tag1, Tag2", LastUpdateOn = DateTime.Now },
                    new KnowledgeBaseItem { Id = 2, Query = "Question2", Answer = "Answer2", Tags = "Tag2, Tag3", LastUpdateOn = DateTime.Now }
              });

            var results = Wispero.Data.TagHelper.Process(queryService.Object, out max);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count == 3);
            Assert.IsTrue(max == 2);
        }

        [TestMethod]
        public void DirectCallWithoutDependencies()
        {
            int max;
            var collection = 
              new System.Collections.Generic.List<KnowledgeBaseItem>() {
                    new KnowledgeBaseItem { Id = 1, Query = "Question1", Answer = "Answer1", Tags = "Tag1, Tag2", LastUpdateOn = DateTime.Now },
                    new KnowledgeBaseItem { Id = 2, Query = "Question2", Answer = "Answer2", Tags = "Tag2, Tag3", LastUpdateOn = DateTime.Now },
                    new KnowledgeBaseItem { Id = 2, Query = "Question2", Answer = "Answer2", Tags = "Tag4, Tag2, Tag3", LastUpdateOn = DateTime.Now }
              };

            var results = Wispero.Data.TagHelper.Process(collection, out max);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count == 4);
            Assert.IsTrue(max == 3);
        }
    }
}
