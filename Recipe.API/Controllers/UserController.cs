﻿using Microsoft.AspNetCore.Mvc;
using Recipe.API.Models;
using Recipe.API.Repository;

namespace Recipe.API.Controllers
{
    public class UserController : Controller
    {
        [Route("api/[controller]")]
        [ApiController]
        public class AccountController : ControllerBase
        {
            private readonly IAccountRepository _accountRepository;

            public AccountController(IAccountRepository accountRepository)
            {
                _accountRepository = accountRepository;
            }

            [HttpPost("signup")]
            public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
            {
                var result = await _accountRepository.SignUpAsync(signUpModel);

                if (result.Succeeded)
                {
                    return Ok(result.Succeeded);
                }

                return Unauthorized();
            }
        }
    }
    }