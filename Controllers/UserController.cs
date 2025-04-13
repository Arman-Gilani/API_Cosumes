using API_Cosumes.BAL;
using API_Cosumes.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_Cosumes.Controllers
{
	public class UserController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5252/api");
        private readonly HttpClient _client;

        CommonFunction cv = new CommonFunction();

        public UserController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region GetAllData
        [HttpGet]
        public IActionResult GET()
        {
			
			List<UserModel> users = new List<UserModel>();
            HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/User/Get").Result;
            if (response.IsSuccessStatusCode)
            {
				users = cv.commomFunction(users, response);
            }
            return View("UserList",users);
        }
        #endregion

        #region GET_CITY_BY_StateID
        [HttpGet]
        public IActionResult GET_CITY_BY_StateID(int StateID)
        {
            List<UserModel> users = new List<UserModel>();
            HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/User/Get_CITY_BY_STATE/{StateID}").Result;
            if (response.IsSuccessStatusCode)
            {
                users = cv.commomFunction(users, response);

				List<LOC_CityDropdownModel> Citylist = new List<LOC_CityDropdownModel>();
                foreach (var item in users)
                {
                    LOC_CityDropdownModel list = new LOC_CityDropdownModel();
                    list.CityID = Convert.ToInt32(item.CityID);
                    list.CityName = Convert.ToString(item.CityName);
                    Citylist.Add(list);
                }
                var vModel = Citylist;
                return Json(vModel);
            }
            return View("CREATE", users);
        }
        #endregion

        #region Add/Edit View
        [HttpGet]
        public IActionResult CREATE() {
            return View();
        }
        #endregion

        #region Get ID if available
        [HttpGet]
        public IActionResult Edit(int id)
        {

            List<UserModel> users = new List<UserModel>();
            HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/User/Get_ALL_States").Result;
            if (response.IsSuccessStatusCode)
            {
				users = cv.commomFunction(users, response);
			}
            List<LOC_StateDropDownModel> StateDropDownlist = new List<LOC_StateDropDownModel>();

            foreach (var item in users)
            {

                LOC_StateDropDownModel list = new LOC_StateDropDownModel();
                list.StateID = Convert.ToInt32(item.StateID);
                list.StateName = Convert.ToString(item.StateName);
                StateDropDownlist.Add(list);
            }
            ViewBag.StateList = StateDropDownlist;

            List<LOC_CityDropdownModel> CityDropDownlist = new List<LOC_CityDropdownModel>();
            ViewBag.CityList = CityDropDownlist;

            UserModel user = null;
            HttpResponseMessage res = _client.GetAsync($"{_client.BaseAddress}/User/Get/{id}").Result;
            if (res.IsSuccessStatusCode)
            {
				user = cv.commomFunction(user, res);

				HttpResponseMessage resAfterSelect = _client.GetAsync($"{_client.BaseAddress}/User/Get_CITY_BY_STATE/{user.StateID}").Result;
                if (resAfterSelect.IsSuccessStatusCode)
                {
					users = cv.commomFunction(users, resAfterSelect);

					List<LOC_CityDropdownModel> Citylist = new List<LOC_CityDropdownModel>();
                    foreach (var item in users)
                    {
                        LOC_CityDropdownModel list = new LOC_CityDropdownModel();
                        list.CityID = Convert.ToInt32(item.CityID);
                        list.CityName = Convert.ToString(item.CityName);
                        Citylist.Add(list);
                    }
                    ViewBag.CityList = Citylist;
                }
            }
            return View("CREATE", user);
        }
        #endregion

        #region Save
        [HttpPost]
        public async Task<IActionResult> Save(UserModel modelUser) {

            try
            {
                MultipartFormDataContent formData = new MultipartFormDataContent();
                formData.Add(new StringContent(modelUser.Name), "Name");
                formData.Add(new StringContent(modelUser.Contact), "Contact");
                formData.Add(new StringContent(modelUser.Email), "Email");
                formData.Add(new StringContent(modelUser.StateID.ToString()), "StateID");
                formData.Add(new StringContent(modelUser.CityID.ToString()), "CityID");

                if (modelUser.UserId==0)
                {
                    HttpResponseMessage res = await _client.PostAsync($"{_client.BaseAddress}/User/Post", formData);
                    if (res.IsSuccessStatusCode)
                    {
                        TempData["message"] = "User Inserted";
                        return RedirectToAction("GET");
                    }
                }
                else
                { 
                    HttpResponseMessage response = await _client.PutAsync($"{_client.BaseAddress}/User/Put/{modelUser.UserId}", formData);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["message"] = "User Updated";
                        return RedirectToAction("GET");
                    }
                }
                
            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred: " + ex.Message;
            }

            return RedirectToAction("GET");
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{_client.BaseAddress}/User/Delete/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["message"] = "User Deleted";
            }
            return RedirectToAction("GET");
        }
        #endregion

    }

}

