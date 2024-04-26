using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using Movies.Models.LoginRequest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Movies.Controllers
{
    public class Movies1Controller : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7205/api");
        private readonly HttpClient _httpClient;

        public Movies1Controller()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<MoviesReviews> moviesReviews = new List<MoviesReviews>();

            var token = HttpContext.Session.GetString("Token");

            if (token == null)
            {
                return RedirectToAction("Login");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/Movie/GetAll");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                moviesReviews = JsonConvert.DeserializeObject<List<MoviesReviews>>(data);
            }


            return View(moviesReviews);
        }

        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "/UserAuth/Login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                dynamic jsonResponse = JsonConvert.DeserializeObject(responseData);

                string token = jsonResponse.result.token;

                if (!string.IsNullOrEmpty(token))
                {
                    HttpContext.Session.SetString("Token", token);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Token is empty");
                    return View("Login");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View("Login");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequest registrationRequest)

        {
            var user = new LocalUser
            {
               
                UserName= registrationRequest.UserName,
                Name= registrationRequest.Name,
                Pasword= registrationRequest.Password,
                Role= registrationRequest.Role,

            };
            if (ModelState.IsValid)
            {

                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "/UserAuth/Register", registrationRequest);

                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction("Login");
                }
                else
                {

                    ModelState.AddModelError(string.Empty, "Failed to register. Please try again.");
                   
                }
            }


            return View(Login);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MoviesReviews newReview)
        {
            var token = HttpContext.Session.GetString("Token");

            if (token == null)
            {
                return RedirectToAction("Login");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "/Movie/Create", newReview);

            if (response.IsSuccessStatusCode)
            {
                // If the request is successful, redirect to the Index action to show the updated list
                return RedirectToAction("Index");
            }
            else
            {
                // If the request is not successful, handle the error (e.g., display an error message)
                // You may want to deserialize the response content to get error details
                string errorData = await response.Content.ReadAsStringAsync();
                // Handle the error appropriately, such as displaying an error message
                // For simplicity, let's assume an error message is displayed in the view
                ViewData["ErrorMessage"] = "Error occurred while adding the review: " + errorData;
                return View(newReview); // Return the view with the model to allow the user to correct the input
            }
        }

    }
}
