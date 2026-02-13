using System;

enum TipoBiglietto
{
    standard,
    ridotto,
    vip
}

enum StatoPrenotazione
{
    prenotato,
    pagato,
    annullato
}

struct Biglietto
{
    public int posto;
    public TipoBiglietto tipo;
    public StatoPrenotazione stato;
    public DateTime data;

    public Biglietto(int p, TipoBiglietto t, StatoPrenotazione s, DateTime d)
    {
        posto = p;
        tipo = t;
        stato = s;
        data = d;
    }

    public string Stampa()
    {
        return $"Posto: {posto}, Tipo del biglietto: {tipo}, Stato: {stato}, Data: {data}";
    }

    public void ModificaStato(StatoPrenotazione nuovoStato)
    {
        stato = nuovoStato;
    }
}

class Program
{
    static void Main()
    {
        Biglietto[] b = new Biglietto[50];
        int numb = 0;

        int n = NumeroBiglietti();

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nCreazione biglietto {i + 1}:");
            b[numb] = CreaBiglietto();
            numb++;
        }

        Stampa(b, numb, "Biglietti creati:");

        if (Conferma("Vuoi pagare un biglietto?"))
        {
            PagaBiglietto(b, numb);
        }

        if (Conferma("Vuoi annullare un biglietto?"))
        {
            AnnullaBiglietto(b, numb);
        }

        Stampa(b, numb, "Stato finale:");
    }

    static int NumeroBiglietti()
    {
        Console.WriteLine("Quanti biglietti vuoi creare? Il numero massimo è 50");
        int n;
        while (!int.TryParse(Console.ReadLine(), out n) || n < 1 || n > 50)
        {
            Console.Write("Inserisci un numero tra 1 e 50: ");
        }
        return n;
    }

    static int Posto()
    {
        Console.Write("Inserisci il numero del posto: ");
        int p;
        while (!int.TryParse(Console.ReadLine(), out p) || p <= 0)
        {
            Console.Write("Inserisci un numero di posto valido: ");
        }
        return p;
    }

    static TipoBiglietto Tipo()
    {
        Console.Write("Inserisci il tipo di biglietto: ");
        string tipoString = Console.ReadLine().ToLower();
        while (tipoString != "standard" && tipoString != "ridotto" && tipoString != "vip")
        {
            Console.Write("Scrivi un nome valido: ");
            tipoString = Console.ReadLine().ToLower();
        }

        if (tipoString == "standard")
            return TipoBiglietto.standard;
        else if (tipoString == "ridotto")
            return TipoBiglietto.ridotto;
        else
            return TipoBiglietto.vip;
    }

    static Biglietto CreaBiglietto()
    {
        int posto = Posto();
        TipoBiglietto tipo = Tipo();
        DateTime data = DateTime.Now;
        StatoPrenotazione stato = StatoPrenotazione.prenotato;

        return new Biglietto(posto, tipo, stato, data);
    }

    static bool Conferma(string messaggio)
    {
        Console.Write(messaggio + " ");
        string r = Console.ReadLine().ToLower();
        return (r == "si" || r == "sì");
    }

    static int TrovaBiglietto(Biglietto[] b, int numb, int posto)
    {
        for (int i = 0; i < numb; i++)
        {
            if (b[i].posto == posto)
                return i;
        }
        return -1; // non trovato
    }

    static void PagaBiglietto(Biglietto[] b, int numb)
    {
        int posto = Posto();
        int indice = TrovaBiglietto(b, numb, posto);

        if (indice == -1)
        {
            Console.WriteLine("Posto non trovato.");
        }
        else if (b[indice].stato != StatoPrenotazione.pagato &&
                 b[indice].stato != StatoPrenotazione.annullato)
        {
            b[indice].ModificaStato(StatoPrenotazione.pagato);
            Console.WriteLine("Biglietto pagato.");
        }
        else
        {
            Console.WriteLine("Biglietto non modificabile.");
        }
    }

    static void AnnullaBiglietto(Biglietto[] b, int numb)
    {
        int posto = Posto();
        int indice = TrovaBiglietto(b, numb, posto);

        if (indice == -1)
        {
            Console.WriteLine("Posto non trovato.");
        }
        else if (b[indice].stato != StatoPrenotazione.annullato)
        {
            b[indice].ModificaStato(StatoPrenotazione.annullato);
            Console.WriteLine("Biglietto annullato.");
        }
        else
        {
            Console.WriteLine("Biglietto già annullato.");
        }
    }

    static void Stampa(Biglietto[] b, int numb, string titolo)
    {
        Console.WriteLine("\n" + titolo);
        for (int i = 0; i < numb; i++)
        {
            Console.WriteLine(b[i].Stampa());
        }
    }