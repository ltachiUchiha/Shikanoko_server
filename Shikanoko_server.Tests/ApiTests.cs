using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shikanoko_server.Tests
{
    [TestClass]
    public class ApiTests
    {

        [TestMethod]
        public async Task getKanji()
        {

            await using var app = new WebApplicationFactory<Program>();
            using var client = app.CreateClient();

            var response = await client.GetAsync("/kanji");


            response.EnsureSuccessStatusCode();
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task getCertainKanji()
        {

            await using var app = new WebApplicationFactory<Program>();
            using var client = app.CreateClient();

            var response = await client.GetAsync("/kanji/1");


            response.EnsureSuccessStatusCode();
            Assert.IsTrue(response.IsSuccessStatusCode);
        }


        [TestMethod]
        public async Task getCertainKanjiWithError()
        {

            await using var app = new WebApplicationFactory<Program>();
            using var client = app.CreateClient();

            var response = await client.GetAsync("/kanji/-1");

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task postKanji()
        {

            await using var app = new WebApplicationFactory<Program>();
            using var client = app.CreateClient();

            var response = await client.PostAsJsonAsync("/kanji", new Kanji(0, "", 0, "", "", ""));

            Assert.IsTrue(response.IsSuccessStatusCode);
        }
        [TestMethod]
        public async Task postKanjiWithError()
        {

            await using var app = new WebApplicationFactory<Program>();
            using var client = app.CreateClient();

            var response = await client.PostAsJsonAsync("/kanji", new Kanji(0, "", 0, "", "", ""));
            response = await client.PostAsJsonAsync("/kanji", new Kanji(0, "", 0, "", "", ""));

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task deleteKanji()
        {

            await using var app = new WebApplicationFactory<Program>();
            using var client = app.CreateClient();

            var response = await client.PostAsJsonAsync("/kanji", new Kanji(0, "", 0, "", "", ""));

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task deleteKanjiWithError()
        {

            await using var app = new WebApplicationFactory<Program>();
            using var client = app.CreateClient();

            var response = await client.DeleteAsync("/kanji/-1");

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }
    }

    internal record Kanji(int id, string literal, int jlpt_level, string ja_on, string ja_kun, string meaning);
}
