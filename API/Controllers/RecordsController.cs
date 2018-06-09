﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;

namespace API.Controllers
{
    public class RecordsController : ApiController
    {
        private Model1 db = new Model1();
  
        // GET: api/Records
        public IQueryable<Record> GetRecords()
        {
            return db.Records;
        }

        [HttpGet]
        [Route("api/gettop10/")]
        public async Task<IQueryable<Record>> GetTop10()
        {

            List<Record> list = await db.Records.ToListAsync<Record>();
            list = list.OrderByDescending(p => p.Score).ThenBy(p => p.Time).ToList();
            return list.Take(10).AsQueryable(); ;

        }

        // GET: api/Records/5
        [ResponseType(typeof(Record))]
        public IHttpActionResult GetRecord(int id)
        {
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        // PUT: api/Records/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRecord(int id, Record record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != record.Id)
            {
                return BadRequest();
            }

            db.Entry(record).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Records
        [ResponseType(typeof(Record))]
        public IHttpActionResult PostRecord(Record record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Records.Add(record);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = record.Id }, record);
        }

        // DELETE: api/Records/5
        [ResponseType(typeof(Record))]
        public IHttpActionResult DeleteRecord(int id)
        {
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return NotFound();
            }

            db.Records.Remove(record);
            db.SaveChanges();

            return Ok(record);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecordExists(int id)
        {
            return db.Records.Count(e => e.Id == id) > 0;
        }
    }
}