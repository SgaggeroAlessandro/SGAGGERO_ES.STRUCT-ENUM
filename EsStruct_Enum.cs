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

        Console.WriteLine("Quanti biglietti vuoi creare? Il numero massimo è 50 ");
        int n;
        while (!int.TryParse(Console.ReadLine(), out n) || n < 1 || n > 50)
        {
            Console.Write("Inserisci un numero tra 1 e 50: ");
        }

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Creazione biglietto {i + 1}:");

            Console.Write("Inserisci il numero del posto: ");
            int p;
            while (!int.TryParse(Console.ReadLine(), out p) || p <= 0)
            {
                Console.Write("Inserisci un numero di posto valido: ");
            }

            Console.Write("Inserisci il tipo di biglietto: ");
            string tipoString = Console.ReadLine().ToLower();
            while (tipoString != "standard" && tipoString != "ridotto" && tipoString != "vip")
            {
                Console.Write("Scrivi un nome valido: ");
                tipoString = Console.ReadLine().ToLower();
            }

            TipoBiglietto tipo;
            if (tipoString == "standard")
            {
                tipo = TipoBiglietto.standard;
            }
            else if (tipoString == "ridotto")
            {
                tipo = TipoBiglietto.ridotto;
            }
            else
            {
                tipo = TipoBiglietto.vip;
            }
            DateTime data = DateTime.Now;
            StatoPrenotazione stato = StatoPrenotazione.prenotato;

            b[numb] = new Biglietto(p, tipo, stato, data);
            numb++;
        }

        Console.WriteLine("Biglietti  creati");
        for (int i = 0; i < numb; i++)
        {
            Console.WriteLine(b[i].Stampa());
        }
  
        Console.Write("Vuoi pagare un biglietto? ");
        string r = Console.ReadLine().ToLower();
        if (r == "si" || r == "sì")
        {
            Console.Write("Inserisci il numero di posto: ");
            int pp;
            while (!int.TryParse(Console.ReadLine(), out pp) || pp <= 0)
            {
                Console.Write(" Numero non valido: ");
            }

            bool t = false;
            for (int i = 0; i < numb; i++)
            {
                if (b[i].posto == pp)
                {
                    if (b[i].stato != StatoPrenotazione.pagato && b[i].stato != StatoPrenotazione.annullato)
                    {
                        b[i].ModificaStato(StatoPrenotazione.pagato);
                        Console.WriteLine("Biglietto pagato.");
                    }
                    else
                    {
                        Console.WriteLine("Biglietto non modificabile.");
                    }
                    t = true;
                    break;
                }
            }
            if (t == false)
            {
                Console.WriteLine("Posto non trovato.");
            }
        }

        
        Console.Write("Vuoi annullare un biglietto? ");
        r = Console.ReadLine().ToLower();
        if (r == "si" || r == "sì")
        {
            Console.Write("Inserisci il numero di posto: ");
            int pa;
            while (!int.TryParse(Console.ReadLine(), out pa) || pa <= 0)
            {
                Console.Write("Errore. Numero valido: ");
            }

            bool t = false;
            for (int i = 0; i < numb; i++)
            {
                if (b[i].posto == pa)
                {
                    if (b[i].stato != StatoPrenotazione.annullato)
                    {
                        b[i].ModificaStato(StatoPrenotazione.annullato);
                        Console.WriteLine("Biglietto annullato.");
                    }
                    else
                    {
                        Console.WriteLine("Biglietto già annullato.");
                    }
                    t = true;
                    break;
                }
            }
            if (t == false)
            {
                Console.WriteLine("Posto non trovato.");
            }
        }

        
        Console.WriteLine("Stato finale:");
        for (int i = 0; i < numb; i++)
        {
            Console.WriteLine(b[i].Stampa());
        }
    }
}
