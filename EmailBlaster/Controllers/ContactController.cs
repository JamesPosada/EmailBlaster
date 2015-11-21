using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmailBlaster;
using EmailBlaster.Models;
using DataAccess;
using System.Web;

namespace EmailBlaster.Controllers
{
    public static class Repo
    {
        private static Repository GetRepo()
        {

            return new Repository();
        }


        private static Repository _Repo;
        public static Repository DataRepo
        {
            get
            {
                if (_Repo == null)
                {
                    _Repo = GetRepo();
                }
                return _Repo;
            }
        }
    }
    [Route("api/contacts")]
    public class ContactController : ApiController
    {


        // GET: api/Contact
        
        [HttpGet()]
        public string GetAllProspects()
        {
            string html="";
            foreach(var cont in Repo.DataRepo.GetContacts().ToList())
            {
               
                html += cont.FirstName + " " + cont.LastName + @"<br />";

            }
            HttpContext.Current.Response.Write( html);
            return html;
        }

            // GET: api/Contacts/5
            public string Get(int id)
        {
            return "value";
        }

        // POST: api/Contacts
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Contacts/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Contacts/5
        public void Delete(int id)
        {
        }
    }



    public class Repository
    {

        public Repository()
        {

            _crmQ = new DataAccess.CrmQueries();
        }
        private CrmQueries _crmQ;
        private IEnumerable<Guid> _doNotEmailIds;

        public IEnumerable<Guid> DoNotEmailIds
        {
            get
            {
                if (_doNotEmailIds == null)
                {
                    _doNotEmailIds = _crmQ.GetDoNotMailList();

                }
                return _doNotEmailIds;
            }

        }
        private IEnumerable<Guid> _allEmailsIds;

        public IEnumerable<Guid> AllEmailsIds
        {
            get
            {
                if (_allEmailsIds == null)
                {
                    _allEmailsIds = _crmQ.GetOmniList();

                }
                return _allEmailsIds;
            }

        }

        private IEnumerable<Guid> _frezzorOnlyIds;

        public IEnumerable<Guid> FrezzorOnlyIds
        {
            get
            {
                if (_frezzorOnlyIds == null)
                {
                    _frezzorOnlyIds = _crmQ.GetFrezzorOnlyList();

                }
                return _frezzorOnlyIds;
            }

        }

        private IEnumerable<Guid> _turnerOnlyIds;

        public IEnumerable<Guid> TurnerOnlyIds
        {
            get
            {
                if (_turnerOnlyIds == null)
                {
                    _turnerOnlyIds = _crmQ.GetTurnerOnlyList();

                }
                return _turnerOnlyIds;
            }

        }

        private List<FrezzorXCrm.Contact> GetAllContacts()
        {

            List<Microsoft.Xrm.Sdk.Entity> list = new List<Microsoft.Xrm.Sdk.Entity>();
            foreach (var con in (TurnerOnlyIds.Concat(FrezzorOnlyIds.Concat(AllEmailsIds))).ToList().AsParallel())
            {
                //var contact = _crmQ.SearchForContact(con);
                //list.Add(contact);

            }
            var res = new List<FrezzorXCrm.Contact>();
            list.ToList().AsParallel().ForAll(C => res.Add(C.ToEntity<FrezzorXCrm.Contact>()));
            return res;
        }

        public List<CrmContactModel> GetContacts()
        {

            var listOfConts = _crmQ.SearchForContact((TurnerOnlyIds.Concat(FrezzorOnlyIds.Concat(AllEmailsIds))).ToList()).AsParallel();
            List<CrmContactModel> LofC = new List<CrmContactModel>();
            listOfConts.ForAll(c => LofC.Add(new CrmContactModel(c)));
            return LofC;
        }
    }
}
