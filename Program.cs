using gestionCompte.Data;
using gestionCompte.Models;
using gestionCompte.Services;
using gestionCompte.Services.Impl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<GestionCompteContext>();
// Services
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ICompteService, CompteService>();
builder.Services.AddScoped<IStatistiquesService, StatistiquesService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<GestionCompteContext>();
    DbInitializer.Initialize(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();

public static class DbInitializer
{
    public static void Initialize(GestionCompteContext context)
    {
        context.Database.EnsureCreated();
        if (context.Comptes.Any())
            return;
        var rnd = new Random();
        var titulaires = new List<Titulaire>();
        for (int i = 1; i <= 10; i++)
            titulaires.Add(new Titulaire { Nom = $"Titulaire {i}" });
        context.Titulaires.AddRange(titulaires);
        context.SaveChanges();
        var comptes = new List<Compte>();
        foreach (var t in titulaires)
        {
            var compte = new Compte
            {
                Numero = "ACC" + t.Id.ToString("D4"),
                TitulaireId = t.Id,
                Type = t.Id % 2 == 0 ? CompteType.EPARGNE : CompteType.COURANT,
                SoldeActuel = rnd.Next(100000, 1000000),
                DateCreation = DateTime.Today.AddDays(-rnd.Next(0, 1000)),
                Statut = t.Id % 3 != 0
            };
            comptes.Add(compte);
        }
        context.Comptes.AddRange(comptes);
        context.SaveChanges();
        foreach (var c in comptes)
        {
            var transactions = new List<Transaction>();
            decimal solde = c.SoldeActuel;
            for (int i = 1; i <= 15; i++)
            {
                var montant = rnd.Next(1000, 50000);
                if (i % 2 == 0) montant *= -1;
                solde += montant;
                transactions.Add(new Transaction
                {
                    CompteId = c.Id,
                    Date = DateTime.Now.AddDays(-i),
                    Montant = montant,
                    SoldeApres = solde,
                    Description = i % 2 == 0 ? "Retrait" : "Dépôt"
                });
            }
            context.Transactions.AddRange(transactions);
            var stat = new Statistiques
            {
                TotalDepots = transactions.Where(x => x.Montant > 0).Sum(x => x.Montant),
                TotalRetraits = transactions.Where(x => x.Montant < 0).Sum(x => -x.Montant),
                NombreTransactions = transactions.Count,
                DerniereTransaction = transactions.Max(x => x.Date)
            };
            context.Statistiques.Add(stat);
        }
        context.SaveChanges();
    }
}
