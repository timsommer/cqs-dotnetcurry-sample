using System;
using Cqs.SampleApp.Core.DataAccess;

namespace Cqs.SampleApp.Core.Domain
{
    public class Book : DbBaseModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime DatePublished { get; set; }
        public bool InMyPossession { get; set; }
    }
}