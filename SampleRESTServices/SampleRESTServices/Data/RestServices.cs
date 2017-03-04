using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;

namespace SampleRESTServices.Data
{
    public class RestServices : IRestService
    {
        HttpClient _client;

        public List<TodoItem> Items { get; private set; }
        public RestServices()
        {
            _client = new HttpClient();
        }

        public async Task<List<TodoItem>> RefreshDataAsync()
        {
            Items = new List<TodoItem>();
            var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
            try
            {
                //objek response
                var response = await _client.GetAsync(uri);
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kesalahan :" + ex.Message);
            }
            return Items;
        }

        public async Task DeleteTodoItemAsync(string id)
        {
            var uri = new Uri(string.Format(Constants.RestUrl, id));
            try
            {
                var response = await _client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"Todoitem berhasil didelete !");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kesalahan : " + ex.Message);
            }
        }

       

        public async Task SaveTodoItemAsync(TodoItem item, bool isNewItem)
        {
            var uriPost = new Uri(string.Format(Constants.RestUrl, string.Empty));
            var uriPut = new Uri(string.Format(Constants.RestUrl, item.ID));

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await _client.PostAsync(uriPost, content);
                }
                else
                {
                    response = await _client.PutAsync(uriPut, content);
                }

                if(response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"Todoitem berhasil disimpan.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kesalahan : " + ex.Message);
            }
        }
    }
}
