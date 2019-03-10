﻿using System;
using System.Collections.Generic;
using AccountingNotebook.BLL.Dtos;
using AccountingNotebook.BLL.Services.Interfaces;
using AccountingNotebook.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AccountingNotebook.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        #region GetAllTransactions

        [HttpGet]
        public IActionResult GetAllTransactions()
        {
            try
            {
                var transactions = _transactionService.GetAllTransactions();
                return Ok(transactions);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        #endregion

        #region GetTransactionById

        [HttpGet]
        public IActionResult GetTransactionById(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                var transactions = _transactionService.GetTransactionById(id);
                return Ok(transactions);
            }
            catch (KeyNotFoundException e)
            {
                return BadRequest(e);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        #endregion

        #region CommitTransaction

        [HttpPost]
        public IActionResult CommitTransaction(TransactionDto transaction)
        {
            try
            {
                if (transaction?.TransactionType == null)
                {
                    return BadRequest();
                }

                var transactionType = EnumParser.ParseToTransactionType(transaction.TransactionType);
                var transactionDto = _transactionService.CommitTransaction(transaction.Amount, transactionType);
                return Ok(transactionDto);
            }
            catch (ArgumentException)
            {
                return StatusCode(403);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        #endregion

    }
}