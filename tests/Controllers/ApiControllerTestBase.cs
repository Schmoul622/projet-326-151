using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Epsic.Gestion_artistes.Rpg;
using Epsic.Gestion_artistes.Rpg.Data;
using Epsic.Gestion_artistes.Rpg.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace tests.Controllers
{
    public class CustomWebApplicationFactory<TStartup> 
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<EpsicGestionArtisteRpgDataContext>));
                if (descriptor == null) return;
                services.Remove(descriptor);

                services.AddDbContext<EpsicGestionArtisteRpgDataContext>(options =>
                {
                    options.UseInMemoryDatabase("Epsic.Gestion_artistes.Rpg");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<EpsicGestionArtisteRpgDataContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        ResetInMemoryDatabse(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, ex.Message);
                    }
                }
            });
        }

        private static void ResetInMemoryDatabse(EpsicGestionArtisteRpgDataContext db)
        {
            db.RemoveRange(db.Albums);
            db.SaveChanges();

            db.AddRange(new List<Album>
            {
                new Album
                {
                    Id = 1, Name = "Flip", NumberTitles = 12, ReleaseDate = 2016, Duration = "45min"
                },
                new Album
                {
                    Id = 2, Name = "Jeannine", NumberTitles = 15, ReleaseDate = 2018, Duration = "90min"
                },
                new Album
                {
                    Id = 3, Name = "Amina", NumberTitles = 32, ReleaseDate = 2019, Duration = "125min"
                },
            });
            db.SaveChanges();
        }
    }
    
    public class ApiControllerTestBase : CustomWebApplicationFactory<Startup>
    {
        private CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;
        
        [TestInitialize]
        public void SetupTest()
        {
            _factory = new CustomWebApplicationFactory<Startup>();
            _client = _factory.CreateClient();
        }

        protected async Task<HttpResponseMessage> GetAsync(string url) 
        {
            return await _client.GetAsync(url);
        } 

        protected async Task<T> GetAsync<T>(string url) 
        {
            var response = await _client.GetAsync(url);

            var body = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(body);
        }

        protected async Task<HttpResponseMessage> PostBasicAsync<T>(string url, T body) 
        {
            return await _client.PostAsJsonAsync(url, body);
        }

        protected async Task<HttpResponseMessage> PostFileAsync(string url, byte[] file) 
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(file), "file", "filename");
            return await _client.PostAsync(url, content);
        }

        protected async Task<T> PostAsync<T>(string url, T body) 
        {
            return await PostAsync<T, T>(url, body);
        }

        protected async Task<U> PostAsync<T, U>(string url, T body) 
        {
            var response = await _client.PostAsJsonAsync(url, body);

            return await response.Content.ReadFromJsonAsync<U>();
        }

        protected async Task<HttpResponseMessage> DeleteAsync(string url) 
        {
            var response = await _client.DeleteAsync(url);

            return response.EnsureSuccessStatusCode();
        } 
    }
    
    public partial class RfcError
    {
        public Uri Type { get; set; }

        public string Title { get; set; }

        public long Status { get; set; }

        public string TraceId { get; set; }

        public Error Errors { get; set; }
    }

    public partial class Error
    {
        public string[] Name { get; set; }
    }
}