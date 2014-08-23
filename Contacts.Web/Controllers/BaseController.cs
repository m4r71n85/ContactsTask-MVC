using Contacts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contacts.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IUowData Data;
        private int pageSize = 20;
        public BaseController()
            : this(new UowData())
        {
        }
        public BaseController(IUowData data)
        {
            this.Data = data;
        }

        public int PageSize { 
            get{ return pageSize; }
        }
    }
}