using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


var kanji_list = new List<Kanji>
{
    new Kanji(1, "山", 4, "サン", "やま", "mountain"),
    new Kanji(2, "川", 4, "セン", "かわ", "river"),
    new Kanji(3, "月", 4, "ガツ", "つき", "moon"),
    new Kanji(4, "日", 4, "ニチ", "ひ", "sun")
};

app.MapGet("/kanji", () =>
{
    return kanji_list.ToArray();
})
.WithName("GetAllKanjis")
.WithOpenApi();

app.MapGet("/kanji/{kanji_id}", Results<BadRequest<string>, Ok<Kanji>>
    (int kanji_id) =>
{
    if (kanji_list.Exists(x => x.id == kanji_id))
        return TypedResults.Ok(kanji_list.First(x => x.id == kanji_id));
    
    return TypedResults.BadRequest("There is no kanji with the given id");

})
.WithName("GetKanjiById")
.WithOpenApi();

app.MapPost("/kanji", Results<BadRequest, Ok> (Kanji kanji) =>
{
    if(!kanji_list.Exists(x => x.id == kanji.id))
    {
        kanji_list.Add(kanji);
        return TypedResults.Ok();
    }
    return TypedResults.BadRequest();
});


app.MapDelete("/kanji/{kanji_id}", Results<BadRequest, Ok> (int kanji_id) =>
{
    if (kanji_list.Exists(x => x.id == kanji_id))
    {
        kanji_list.Remove(kanji_list.First(x => x.id == kanji_id));
        return TypedResults.Ok();
    }
    return TypedResults.BadRequest();
});

app.Urls.Add("http://*:30000");
app.Run();

internal record Kanji(int id, string literal, int jlpt_level, string ja_on, string ja_kun, string meaning);
