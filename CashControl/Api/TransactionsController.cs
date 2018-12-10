 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 using CashControl.Models;
 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashControl.Api
{
    public class TransactionsController : Controller
    {
        private readonly CashControlContext _cashControlContext;
        public TransactionsController(CashControlContext cashControlContext)
        {
            _cashControlContext = cashControlContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetById(int id)
        {
            var result = _cashControlContext.Transactions.Where(t => t.Id == id).FirstOrDefault();
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(_cashControlContext.Transactions.ToList());
        }
        
        [HttpPost]
        public async Task<ActionResult> Add(Transactions transaction)
        {
            try
            {
                var result = _cashControlContext.Transactions.Add(transaction);
                await _cashControlContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(Transactions transaction)
        {
            try
            {
                var result = _cashControlContext.Transactions.Where(t => t.Id == transaction.Id).FirstOrDefault();
                if (result == null)
                    return BadRequest();
                result = transaction;
                await _cashControlContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        [HttpDelete]
        public async Task<ActionResult> Remove(int transactionId)
        {
            try
            {
                var deletedEntity = _cashControlContext.Transactions.Where(t => t.Id == transactionId).FirstOrDefault();
                var result = _cashControlContext.Transactions.Remove(deletedEntity);
                await _cashControlContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}