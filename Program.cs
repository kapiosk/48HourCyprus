using AspNetStatic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddSingleton<IStaticResourcesInfoProvider>(
  new StaticResourcesInfoProvider(
    [
      new PageResource("/index"),
      new PageResource("/donations"),
      new CssResource("/css/site.css" ),
      new BinResource("/images/48hr.png" ),
      new BinResource("/images/IMG_3984.jpg" ),
      new BinResource("/images/IMG_8910-1.jpg" ),
      // new JsResource("/js/site.js"){ OptimizerType = OptimizerType.Js },
      // new BinResource("/favicon.ico" ){ OptimizerType = OptimizerType.Bin },
    ]));

await using var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

if (args.Contains("-generate"))
  app.GenerateStaticContent("src", exitWhenDone: true);

await app.RunAsync();
