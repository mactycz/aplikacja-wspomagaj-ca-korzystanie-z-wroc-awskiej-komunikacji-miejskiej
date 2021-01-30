using System;
using System.Collections.Generic;

namespace praca_inż
{
    static class menu
    {
        public static void startmenu()
        {

            readgtfs stop_times = new readgtfs();
            stop_times.readfile("stop_times", 7);

            readgtfs trips = new readgtfs();
            trips.readfile("trips", 9);

            readgtfs stops = new readgtfs();
            stops.readfile("stops", 5);

            readgtfs calendar = new readgtfs();
            calendar.readfile("calendar", 10);


            string linia;
            string[] dzien = new string[7];
            string inptdzien;
            int poprawnywariant = 0;
            string kierunek;
            string przystanek;
            List<string> wyniki = new List<string>();
            List<string> tymczasowe_wyniki = new List<string>();
            List<string> godziny = new List<string>();
            while (true)
            {
                Console.Write('\n');
                Console.WriteLine("Wybierz opcje: ");
                Console.WriteLine("1. Podgląd lini");
                Console.WriteLine("2. Podgląd przystanku");
                Console.WriteLine("3. Znajdź najlepsze połączenie");
                Console.WriteLine("4. Wyjdź z programu");
                ConsoleKeyInfo klawisz = Console.ReadKey();

                switch (klawisz.Key)
                {

                    case ConsoleKey.D1:
                        Console.Clear();


                        for (int k = 1; k < trips.lineCount; k++)
                        {
                            if (k > 1 && trips.result[k, 0] == trips.result[k - 1, 0])
                            {

                            }
                            else
                            {
                                Console.Write(trips.result[k, 0]);
                                Console.Write('\n');
                            }
                        }

                        Console.WriteLine("Wpisz linie aby poznać jej dokładny rozklad: ");
                        linia = Console.ReadLine();


                        if (linia == "")
                        {
                            Console.Clear();
                        }
                        else
                        {

                            Console.Clear();
                            Console.WriteLine("Wybierz dzień tygodnia:");

                            dzien[0] = "Poniedziałek";
                            dzien[1] = "Wtorek";
                            dzien[2] = "Środa";
                            dzien[3] = "Czwartek";
                            dzien[4] = "Piątek";
                            dzien[5] = "Sobota";
                            dzien[6] = "Niedziela";
                            for (int idz = 0; idz < 7; idz++)
                            {
                                Console.WriteLine(idz + 1 + " " + dzien[idz]);
                            }
                            inptdzien = Console.ReadLine();
                            Console.Clear();
                            for (int v = 1; v < calendar.lineCount; v++)
                            {
                                if (inptdzien != "" && Int32.Parse(calendar.result[v, Int32.Parse(inptdzien)]) == 1)
                                {

                                    poprawnywariant = Int32.Parse(calendar.result[v, 0]);

                                }

                            }




                            for (int p = 0; p < trips.lineCount; p++)
                            {

                                if (linia == trips.result[p, 0].ToLower() || linia == trips.result[p, 0])
                                {
                                    if (Int32.Parse(trips.result[p, 1]) == poprawnywariant)
                                    {

                                        if (!tymczasowe_wyniki.Contains(trips.result[p, 3]))
                                        {
                                            tymczasowe_wyniki.Add(trips.result[p, 3]);
                                        }



                                    }
                                }
                            }
                            tymczasowe_wyniki.ForEach(i => Console.WriteLine("{0}\t", i));
                            tymczasowe_wyniki.Clear();

                            Console.WriteLine("Wybierz kierunek");
                            kierunek = Console.ReadLine();

                            Console.Clear();
                            for (int p = 0; p < trips.lineCount; p++)
                            {

                                if (linia == trips.result[p, 0].ToLower() || linia == trips.result[p, 0])
                                {
                                    if (trips.result[p, 3] == "\"" + kierunek + "\"" || trips.result[p, 3].ToLower() == "\"" + kierunek + "\"")
                                    {



                                        for (int w = 1; w < stop_times.lineCount; w++)
                                        {
                                            if (trips.result[p, 2] == stop_times.result[w, 0])
                                            {
                                                for (int r = 1; r < stops.lineCount; r++)
                                                {
                                                    if (stop_times.result[w, 3] == stops.result[r, 0])
                                                    {
                                                        if (!tymczasowe_wyniki.Contains(stops.result[r, 2]))
                                                        {
                                                            tymczasowe_wyniki.Add(stops.result[r, 2]);
                                                        }



                                                    }
                                                }
                                            }
                                        }
                                    }


                                }
                            }
                            tymczasowe_wyniki.ForEach(i => Console.WriteLine("{0}\t", i));
                            tymczasowe_wyniki.Clear();

                            Console.WriteLine("Wybierz przystanek");
                            przystanek = Console.ReadLine();

                            Console.Clear();
                            for (int i = 0; i < trips.lineCount; i++)
                            {
                                if ((trips.result[i, 0] == linia || trips.result[i, 0].ToLower() == linia) && (trips.result[i, 3] == "\"" + kierunek + "\"" || trips.result[i, 3].ToLower() == "\"" + kierunek + "\"") && Int32.Parse(trips.result[i, 1]) == poprawnywariant)
                                {
                                    for (int j = 0; j < stop_times.lineCount; j++)
                                    {
                                        if (trips.result[i, 2] == stop_times.result[j, 0])
                                        {
                                            for (int k = 0; k < stops.lineCount; k++)
                                            {
                                                if ("\"" + przystanek + "\"" == stops.result[k, 2].ToLower() || "\"" + przystanek + "\"" == stops.result[k, 2])
                                                {
                                                    if (stops.result[k, 0] == stop_times.result[j, 3])
                                                    {
                                                        wyniki.Add(stop_times.result[j, 1]);
                                                    }

                                                }
                                            }

                                        }
                                    }
                                }

                            }
                            if (wyniki == null)
                            {
                                Console.WriteLine("pusto");
                            }
                            else
                            {
                                Console.WriteLine(linia + " " + kierunek + " " + przystanek);
                                wyniki.Sort();
                                wyniki.ForEach(i => Console.WriteLine("{0}\t", i));
                                wyniki.Clear();
                            }



                        }

                        break;
                    case ConsoleKey.D2:
                        Console.Clear();

                        Console.WriteLine("Wybierz dzień tygodnia:");

                        dzien[0] = "Poniedziałek";
                        dzien[1] = "Wtorek";
                        dzien[2] = "Środa";
                        dzien[3] = "Czwartek";
                        dzien[4] = "Piątek";
                        dzien[5] = "Sobota";
                        dzien[6] = "Niedziela";
                        for (int idz = 0; idz < 7; idz++)
                        {
                            Console.WriteLine(idz + 1 + " " + dzien[idz]);
                        }
                        inptdzien = Console.ReadLine();
                        if (inptdzien == "")
                        {
                            Console.WriteLine("Niepoprawne dane");
                        }
                        else
                        {
                            Console.Clear();
                            for (int v = 1; v < calendar.lineCount; v++)
                            {
                                if (Int32.Parse(calendar.result[v, Int32.Parse(inptdzien)]) == 1)
                                {


                                    poprawnywariant = Int32.Parse(calendar.result[v, 0]);

                                }

                            }
                            Console.WriteLine("Wybierz przystanek: ");
                            przystanek = Console.ReadLine();

                            for (int i = 0; i < stops.lineCount; i++)
                            {
                                if ("\"" + przystanek + "\"" == stops.result[i, 2].ToLower() || "\"" + przystanek + "\"" == stops.result[i, 2])
                                {
                                    for (int j = 0; j < stop_times.lineCount; j++)
                                    {
                                        if (stops.result[i, 0] == stop_times.result[j, 3])
                                        {
                                            for (int k = 0; k < trips.lineCount; k++)
                                            {
                                                if (stop_times.result[j, 0] == trips.result[k, 2] && Int32.Parse(trips.result[k, 1]) == poprawnywariant)
                                                {

                                                    if (!tymczasowe_wyniki.Contains(trips.result[k, 0] + " " + trips.result[k, 3]))
                                                    {
                                                        tymczasowe_wyniki.Add(trips.result[k, 0] + " " + trips.result[k, 3]);
                                                    }


                                                }
                                            }
                                        }
                                    }
                                }

                            }
                            tymczasowe_wyniki.Sort();
                            tymczasowe_wyniki.ForEach(i => Console.WriteLine("{0}\t", i));
                            tymczasowe_wyniki.Clear();




                            Console.WriteLine("Wybierz linie");
                            linia = Console.ReadLine();
                            Console.WriteLine("Wybierz kierunek");
                            kierunek = Console.ReadLine();
                            Console.Clear();
                            for (int i = 0; i < trips.lineCount; i++)
                            {
                                if ((trips.result[i, 0] == linia || trips.result[i, 0].ToLower() == linia) && (trips.result[i, 3] == "\"" + kierunek + "\"" || trips.result[i, 3].ToLower() == "\"" + kierunek + "\"") && Int32.Parse(trips.result[i, 1]) == poprawnywariant)
                                {
                                    for (int j = 0; j < stop_times.lineCount; j++)
                                    {
                                        if (trips.result[i, 2] == stop_times.result[j, 0])
                                        {
                                            for (int k = 0; k < stops.lineCount; k++)
                                            {
                                                if ("\"" + przystanek + "\"" == stops.result[k, 2].ToLower() || "\"" + przystanek + "\"" == stops.result[k, 2])
                                                {
                                                    if (stops.result[k, 0] == stop_times.result[j, 3])
                                                    {
                                                        wyniki.Add(stop_times.result[j, 1]);
                                                    }

                                                }
                                            }

                                        }
                                    }
                                }

                            }



                            Console.WriteLine(linia + " " + kierunek + " " + przystanek);
                            wyniki.Sort();
                            wyniki.ForEach(i => Console.WriteLine("{0}\t", i));
                            wyniki.Clear();




                        }

                        break;
                    case ConsoleKey.D3:
                        Console.Clear();

                        Console.WriteLine("Wybierz dzień tygodnia:");

                        dzien[0] = "Poniedziałek";
                        dzien[1] = "Wtorek";
                        dzien[2] = "Środa";
                        dzien[3] = "Czwartek";
                        dzien[4] = "Piątek";
                        dzien[5] = "Sobota";
                        dzien[6] = "Niedziela";
                        for (int idz = 0; idz < 7; idz++)
                        {
                            Console.WriteLine(idz + 1 + " " + dzien[idz]);
                        }
                        inptdzien = Console.ReadLine();

                        Console.Clear();
                        for (int v = 1; v < calendar.lineCount; v++)
                        {
                            if (Int32.Parse(calendar.result[v, Int32.Parse(inptdzien)]) == 1 && inptdzien != null)
                            {


                                poprawnywariant = Int32.Parse(calendar.result[v, 0]);

                            }

                        }
                        Console.WriteLine("Podaj przystanek z którego chcesz odjechać");
                        string odjazd = Console.ReadLine();
                        Console.WriteLine("Podaj przystanek do którego chcesz dojechać");
                        string dojazd = Console.ReadLine();
                        Console.WriteLine("Podaj godzine (hh)");
                        string godzina_input = Console.ReadLine();
                        Console.WriteLine("Podaj minute (mm)");
                        string minuta_input = Console.ReadLine();
                        TimeSpan godzina = new TimeSpan(Int32.Parse(godzina_input), Int32.Parse(minuta_input), 00);
                        TimeSpan limit = new TimeSpan(2, 00, 00);
                        TimeSpan zero = new TimeSpan(0, 0, 0);
                        TimeSpan jeden = new TimeSpan(0, 01, 0);
                        List<string> dojazd_id = new List<string>();
                        List<string> odjazd_id = new List<string>();
                        List<string> wyniki_godzin = new List<string>();
                        List<string> godziny_doj = new List<string>();
                        List<string> dojazdy = new List<string>();
                        Console.Clear();

                        for (int i = 1; i < stops.lineCount; i++)
                        {
                            if (stops.result[i, 2].ToLower() == "\"" + dojazd + "\"" || stops.result[i, 2] == "\"" + dojazd + "\"")
                            {
                                dojazd_id.Add(stops.result[i, 0]);
                                // Console.WriteLine("dojazd dziala");

                            }
                            else if (stops.result[i, 2].ToLower() == "\"" + odjazd + "\"" || stops.result[i, 2] == "\"" + odjazd + "\"")
                            {
                                odjazd_id.Add(stops.result[i, 0]);
                                //Console.WriteLine("odjazd dziala");
                            }

                        }
                        for (int j = 0; j < dojazd_id.Count; j++)
                        {
                            for (int i = 1; i < stop_times.lineCount; i++)
                            {


                                if (stop_times.result[i, 3] == dojazd_id[j])
                                {
                                    if (!tymczasowe_wyniki.Contains(stop_times.result[i, 0]))
                                    {
                                        tymczasowe_wyniki.Add(stop_times.result[i, 0]);
                                        dojazdy.Add(stop_times.result[i, 1]);
                                        // Console.WriteLine("dodano wynik do porownania");
                                    }
                                }

                            }
                        }

                        for (int i = 0; i < stop_times.lineCount; i++)
                        {
                            for (int j = 0; j < odjazd_id.Count; j++)
                            {

                                if (stop_times.result[i, 3] == odjazd_id[j])
                                {
                                    for (int k = 0; k < tymczasowe_wyniki.Count; k++)
                                    {
                                        TimeSpan odj = new TimeSpan(Int32.Parse(stop_times.result[i, 1].Substring(0, 2)), Int32.Parse(stop_times.result[i, 1].Substring(3, 2)), 00);
                                        TimeSpan doj = new TimeSpan(Int32.Parse(dojazdy[k].Substring(0, 2)), Int32.Parse(dojazdy[k].Substring(3, 2)), 00);
                                        if (tymczasowe_wyniki[k] == stop_times.result[i, 0] && odj < doj)
                                        {
                                            wyniki.Add(stop_times.result[i, 0]);
                                            godziny.Add(stop_times.result[i, 1]);
                                            godziny_doj.Add(dojazdy[k]);

                                        }
                                    }
                                }

                            }
                        }
                        tymczasowe_wyniki.Clear();
                        for (int j = 0; j < trips.lineCount; j++)
                        {
                            for (int k = 0; k < wyniki.Count; k++)
                            {
                                if (trips.result[j, 2] == wyniki[k] && poprawnywariant == Int32.Parse(trips.result[j, 1]))
                                {
                                    tymczasowe_wyniki.Add(godziny[k] + "  " + trips.result[j, 0] + "  " + trips.result[j, 3] + "dojazd o " + godziny_doj[k]);
                                }
                            }
                        }
                        wyniki.Clear();

                        for (int k = 0; k < tymczasowe_wyniki.Count; k++)
                        {
                            TimeSpan time = new TimeSpan(Int32.Parse(tymczasowe_wyniki[k].Substring(0, 2)), Int32.Parse(tymczasowe_wyniki[k].Substring(3, 2)), 00);

                            if (time - godzina > zero)
                            {
                                wyniki_godzin.Add("odjazd za: " + (time - godzina).ToString() + "   " + tymczasowe_wyniki[k]);
                            }


                        }

                        wyniki_godzin.Sort();

                        if (wyniki_godzin.Count > 0)
                        {
                            Console.WriteLine("Odjazd z " + odjazd + " do " + dojazd + " o godzinie " + godzina_input + ":" + minuta_input);
                            if (wyniki_godzin.Count > 10)
                            {
                                for (int i = 0; i < 10; i++)
                                {
                                    Console.WriteLine(wyniki_godzin[i]);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < wyniki_godzin.Count; i++)
                                {
                                    Console.WriteLine(wyniki_godzin[i]);
                                }
                            }

                            tymczasowe_wyniki.Clear();
                            wyniki_godzin.Clear();
                            dojazdy.Clear();
                            godziny_doj.Clear();
                        }

                        else
                        {
                            tymczasowe_wyniki.Clear();
                            wyniki_godzin.Clear();
                            dojazdy.Clear();
                            godziny_doj.Clear();

                            TimeSpan godzinalini;

                            for (int j = 0; j < dojazd_id.Count; j++)
                            {
                                for (int i = 1; i < stop_times.lineCount; i++)
                                {

                                    godzinalini = new TimeSpan(Int32.Parse(stop_times.result[i, 1].Substring(0, 2)), Int32.Parse(stop_times.result[i, 1].Substring(3, 2)), 00);
                                    if (stop_times.result[i, 3] == dojazd_id[j] && godzinalini - godzina > zero && godzinalini - godzina < limit)
                                    {
                                        for (int z = 1; z < trips.lineCount; z++)
                                        {
                                            if (poprawnywariant == Int32.Parse(trips.result[z, 1]) && trips.result[z, 2] == stop_times.result[i, 0])
                                            {
                                                if (!tymczasowe_wyniki.Contains(stop_times.result[i, 0]))
                                                {
                                                    tymczasowe_wyniki.Add(stop_times.result[i, 0]);//linia 2
                                                    godziny_doj.Add(stop_times.result[i, 1]);//godzina dojazdu linii 2
                                                }

                                            }

                                        }
                                    }
                                }
                            }

                            for (int i = 0; i < odjazd_id.Count; i++)
                            {
                                for (int j = 1; j < stop_times.lineCount; j++)
                                {
                                    godzinalini = new TimeSpan(Int32.Parse(stop_times.result[j, 1].Substring(0, 2)), Int32.Parse(stop_times.result[j, 1].Substring(3, 2)), 00);

                                    if (odjazd_id[i] == stop_times.result[j, 3] && godzinalini - godzina > zero && godzinalini - godzina < limit)
                                    {
                                        for (int k = 1; k < trips.lineCount; k++)
                                        {
                                            if (poprawnywariant == Int32.Parse(trips.result[k, 1]) && trips.result[k, 2] == stop_times.result[j, 0])
                                            {
                                                if (!dojazdy.Contains(stop_times.result[j, 0]))
                                                {
                                                    godziny.Add(stop_times.result[j, 1]);//godzina odjazdu linii 1
                                                    dojazdy.Add(stop_times.result[j, 0]);//linia 1

                                                }
                                            }

                                        }


                                    }
                                }
                            }


                            List<string> tymczasowe_wyniki2 = new List<string>();

                            List<string> dojazdy2 = new List<string>();

                            for (int j = 0; j < dojazdy.Count; j++)
                            {

                                for (int i = 1; i < stop_times.lineCount; i++)
                                {
                                    if (dojazdy[j] == stop_times.result[i, 0])
                                    {
                                        if (new TimeSpan(Int32.Parse(godziny[j].Substring(0, 2)), Int32.Parse(godziny[j].Substring(3, 2)), 00) < new TimeSpan(Int32.Parse(stop_times.result[i, 1].Substring(0, 2)), Int32.Parse(stop_times.result[i, 1].Substring(3, 2)), 00))
                                        {
                                            if (!wyniki.Contains(stop_times.result[i, 3]))
                                            {
                                                wyniki.Add(stop_times.result[i, 3]);//przesiadka od lini1
                                            }

                                        }
                                    }
                                }
                            }
                            List<string> przesiadkalinia2 = new List<string>();
                            for (int j = 0; j < tymczasowe_wyniki.Count; j++)
                            {

                                for (int i = 1; i < stop_times.lineCount; i++)
                                {
                                    if (tymczasowe_wyniki[j] == stop_times.result[i, 0])
                                    {
                                        if (new TimeSpan(Int32.Parse(godziny_doj[j].Substring(0, 2)), Int32.Parse(godziny_doj[j].Substring(3, 2)), 00) > new TimeSpan(Int32.Parse(stop_times.result[i, 1].Substring(0, 2)), Int32.Parse(stop_times.result[i, 1].Substring(3, 2)), 00))
                                        {
                                            if (!przesiadkalinia2.Contains(stop_times.result[i, 3]))
                                            {
                                                przesiadkalinia2.Add(stop_times.result[i, 3]);//przesiadka od lini1
                                            }
                                        }
                                    }
                                }
                            }


                            List<string> przystankiwspolne = new List<string>();
                            List<string> przystankiwspolne2 = new List<string>();
                            List<string> przystankiwspolne3 = new List<string>();
                            List<string> przystankiwspolne4 = new List<string>();
                            List<string> przystankiwspolne_id = new List<string>();
                            List<string> ostatecznewyniki = new List<string>();
                            List<string> opcje2 = new List<string>();
                            List<TimeSpan> godzinaodjazdu = new List<TimeSpan>();
                            List<string> wyniki_nazwa = new List<string>();
                            List<string> przesiadka_nazwa = new List<string>();
                            List<string> wyniki_id = new List<string>();
                            List<string> przesiadka_id = new List<string>();
                            List<string> godzinadojazdu = new List<string>();


                            for (int i = 0; i < wyniki.Count; i++)
                            {
                                for (int j = 0; j < przesiadkalinia2.Count; j++)
                                {
                                    for (int k = 1; k < stops.lineCount; k++)
                                    {
                                        if (przesiadkalinia2[j] == stops.result[k, 0])
                                        {
                                            if (!przesiadka_nazwa.Contains(stops.result[k, 2]))
                                            {
                                                przesiadka_nazwa.Add(stops.result[k, 2]);
                                                przesiadka_id.Add(stops.result[k, 0]);
                                            }

                                        }
                                        if (wyniki[i] == stops.result[k, 0])
                                        {
                                            if (!wyniki_nazwa.Contains(stops.result[k, 2]))
                                            {
                                                wyniki_nazwa.Add(stops.result[k, 2]);
                                                wyniki_id.Add(stops.result[k, 0]);
                                            }

                                        }
                                    }
                                }
                            }

                            for (int i = 0; i < przesiadka_nazwa.Count; i++)
                            {
                                for (int j = 0; j < wyniki_nazwa.Count; j++)
                                {
                                    if (przesiadka_nazwa[i] == wyniki_nazwa[j])
                                    {
                                        if (!przystankiwspolne.Contains(wyniki_nazwa[j]))
                                        {
                                            przystankiwspolne.Add(wyniki_nazwa[j]);
                                        }
                                    }
                                }
                            }

                            for (int i = 0; i < przystankiwspolne.Count; i++)
                            {
                                for (int j = 0; j < stops.lineCount; j++)
                                {
                                    if (przystankiwspolne[i] == stops.result[j, 2])
                                    {
                                        if (!przystankiwspolne_id.Contains(stops.result[j, 0]))
                                        {
                                            przystankiwspolne_id.Add(stops.result[j, 0]);

                                        }
                                    }
                                }
                            }

                            int f = -1;


                            for (int i = 1; i < stop_times.lineCount; i++)
                            {
                                for (int p = 1; p < tymczasowe_wyniki.Count; p++)
                                {
                                    if (stop_times.result[i, 0] == tymczasowe_wyniki[p])
                                    {
                                        for (int j = 0; j < przystankiwspolne_id.Count; j++)
                                        {
                                            if (stop_times.result[i, 3] == przystankiwspolne_id[j])
                                            {

                                                if (new TimeSpan(Int32.Parse(godziny_doj[p].Substring(0, 2)), Int32.Parse(godziny_doj[p].Substring(3, 2)), 00) > new TimeSpan(Int32.Parse(stop_times.result[i, 1].Substring(0, 2)), Int32.Parse(stop_times.result[i, 1].Substring(3, 2)), 00))
                                                {
                                                    if (new TimeSpan(Int32.Parse(stop_times.result[i, 1].Substring(0, 2)), Int32.Parse(stop_times.result[i, 1].Substring(3, 2)), 00) > godzina)
                                                    {


                                                        przystankiwspolne2.Add(przystankiwspolne_id[j]);

                                                        for (int l = 1; l < trips.lineCount; l++)
                                                        {
                                                            if (tymczasowe_wyniki[p] == trips.result[l, 2])
                                                            {
                                                                tymczasowe_wyniki2.Add(trips.result[l, 0] + " " + trips.result[l, 3]);
                                                                f++;
                                                            }
                                                        }
                                                        for (int k = 1; k < stops.lineCount; k++)
                                                        {
                                                            if (przystankiwspolne_id[j] == stops.result[k, 0])
                                                            {
                                                                przystankiwspolne3.Add(stops.result[k, 2]);
                                                            }
                                                        }

                                                        opcje2.Add(stop_times.result[i, 1] + "  linią " + tymczasowe_wyniki2[f] + " z " + przystankiwspolne3[f] + " na " + dojazd + " dojedzie o " + godziny_doj[p]);
                                                        godzinaodjazdu.Add(new TimeSpan(Int32.Parse(stop_times.result[i, 1].Substring(0, 2)), Int32.Parse(stop_times.result[i, 1].Substring(3, 2)), 00));
                                                        godzinadojazdu.Add(godziny_doj[p]);
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            List<string> dojazdy_trip = new List<string>();
                            int e = -1;


                            for (int i = 1; i < stop_times.lineCount; i++)
                            {
                                for (int k = 0; k < dojazdy.Count; k++)
                                {
                                    if (stop_times.result[i, 0] == dojazdy[k])
                                    {
                                        for (int m = 0; m < przystankiwspolne_id.Count; m++)
                                        {

                                            if (stop_times.result[i, 3] == przystankiwspolne_id[m])
                                            {
                                                for (int j = 0; j < opcje2.Count; j++)
                                                {
                                                    if (godzina < godzinaodjazdu[j])
                                                    {
                                                        for (int l = 1; l < trips.lineCount; l++)
                                                        {
                                                            if (dojazdy[k] == trips.result[l, 2])
                                                            {
                                                                dojazdy2.Add(trips.result[l, 0] + " " + trips.result[l, 3]);
                                                                dojazdy_trip.Add(trips.result[l, 2]);

                                                                e++;
                                                            }
                                                        }
                                                        for (int r = 1; r < stops.lineCount; r++)
                                                        {
                                                            if (przystankiwspolne_id[m] == stops.result[r, 0])
                                                            {
                                                                przystankiwspolne4.Add(stops.result[r, 2]);
                                                            }
                                                        }
                                                        if (przystankiwspolne4[e] == przystankiwspolne3[j])
                                                        {

                                                            if (new TimeSpan(Int32.Parse(godziny[k].Substring(0, 2)), Int32.Parse(godziny[k].Substring(3, 2)), 00) < new TimeSpan(Int32.Parse(opcje2[j].Substring(0, 2)), Int32.Parse(opcje2[j].Substring(3, 2)), 00))
                                                            {
                                                                if (new TimeSpan(Int32.Parse(stop_times.result[i, 1].Substring(0, 2)), Int32.Parse(stop_times.result[i, 1].Substring(3, 2)), 00) < new TimeSpan(Int32.Parse(opcje2[j].Substring(0, 2)), Int32.Parse(opcje2[j].Substring(3, 2)), 00))
                                                                {
                                                                    if (new TimeSpan(Int32.Parse(stop_times.result[i, 1].Substring(0, 2)), Int32.Parse(stop_times.result[i, 1].Substring(3, 2)), 00) > new TimeSpan(Int32.Parse(godziny[k].Substring(0, 2)), Int32.Parse(godziny[k].Substring(3, 2)), 00))
                                                                    {

                                                                        ostatecznewyniki.Add("dojazd do " + dojazd + " o " + godzinadojazdu[j] + "  |  " + odjazd + " o godzinie: " + godziny[k] + " linią " + dojazdy2[e] + " na " + przystankiwspolne4[e] + " o godzinie " + stop_times.result[i, 1] + " a następnie " + opcje2[j]);

                                                                    }
                                                                }
                                                            }

                                                        }

                                                    }

                                                }

                                            }
                                        }
                                    }

                                }
                            }



                            List<int> indeksy = new List<int>();

                            ostatecznewyniki.Sort();
                            
                            for (int i = 0; i < ostatecznewyniki.Count; i++)
                            {
                                if (i > 0 && new TimeSpan(Int32.Parse(ostatecznewyniki[i].Substring(ostatecznewyniki[i].IndexOf("nie: ") + 5, 2)), Int32.Parse(ostatecznewyniki[i].Substring(ostatecznewyniki[i].IndexOf("nie: ") + 8, 2)), 00) <= new TimeSpan(Int32.Parse(ostatecznewyniki[i - 1].Substring(ostatecznewyniki[i - 1].IndexOf("nie: ") + 5, 2)), Int32.Parse(ostatecznewyniki[i - 1].Substring(ostatecznewyniki[i - 1].IndexOf("nie: ") + 8, 2)), 00))
                                {
                                    ostatecznewyniki.RemoveAt(i);
                                    i = 0;
                                }
                            }


                            


                            for (int i = 0; i < ostatecznewyniki.Count; i++)
                            {


                                if (i > 0 && ostatecznewyniki[i].Substring(0, 21+dojazd.Length) == ostatecznewyniki[i - 1].Substring(0, 21+dojazd.Length))
                                {

                                    ostatecznewyniki.RemoveAt(i);
                                    i = 0;
                                }
                            


                            }
                            
                            if (ostatecznewyniki.Count > 0)
                            {
                                ostatecznewyniki.ForEach(i => Console.WriteLine("{0}\t", i));

                            }
                            else
                            {
                                Console.WriteLine("Nie znaleziono połączenia");
                            }

                            tymczasowe_wyniki.Clear();
                            wyniki_godzin.Clear();
                            dojazdy.Clear();
                            godziny_doj.Clear();
                            dojazdy2.Clear();
                            ostatecznewyniki.Clear();
                            opcje2.Clear();
                            indeksy.Clear();
                            dojazdy_trip.Clear();
                            godzinadojazdu.Clear();
                            godzinaodjazdu.Clear();
                            przesiadkalinia2.Clear();
                            przystankiwspolne.Clear();
                            przystankiwspolne2.Clear();
                            przystankiwspolne3.Clear();
                            przystankiwspolne4.Clear();
                            przystankiwspolne_id.Clear();
                            wyniki_nazwa.Clear();
                            wyniki_id.Clear();
                            przesiadka_id.Clear();
                            przesiadka_nazwa.Clear();
                        }

                        break;


                    case ConsoleKey.D4:
                        Console.Clear();
                        System.Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }

            }

        }




    }
}


