using System;
using System.Collections.Generic;
using System.Globalization;

namespace bloggen
{
    class Program
    {


        /* ***************************************************************************
         * Kontrollerar värden från användaren. 
         * Tar användarens sträng. Retunerar en bool som indikerar ifall 
         * valideringen lyckats. 
         *****************************************************************************/
        static bool inputKontrollMetod(string input)
        {            
                    if (input.Length <= 0)
                     {  
                        Console.Write("\n\tFelaktigt värde.");
                        return false;
                        
                     }
                    return true;
        }


        /* ***************************************************************************
         * UNDERMENY  
         * Då undermenyn används på två ställen skapade jag en metod för att 
         * hålla nere antalet rader kod som behövs. 
         *****************************************************************************/
        static bool underMeny(List<string[]> bloggen)
        {
            // Menyn. 
            Console.WriteLine("\n\tGör ditt val:" +
                              "\n\t[1]Redigera ett inlägg" +
                              "\n\t[2]Radera ett inlägg" +
                              "\n\t[3]Gå tillbaka");

            int index; // Input av index till radering och redigering. 
            Int32.TryParse(Console.ReadLine(), out int underMeny); // input från menyn. 


            switch (underMeny)
            {
                // Redigera
                case 1:
                    // Ber användaren om input och sparar det i index variabeln. 
                    Console.WriteLine("\n\tVälj index att redigera:");
                    Int32.TryParse(Console.ReadLine(), out index);

                    // Kontrollerar så att indexet finns för att förhindra körtidsfel. 
                    if(bloggen.Count > index)
                    {
                        //Visa inlägget som ska redigeras. 
                        Console.WriteLine("\tRedigera inlägg:\n\t" + bloggen[index][1] + "\n\t" + bloggen[index][2]);

                        //Titel
                        Console.WriteLine("\n\tAnge ny titel:");
                        bloggen[index][1] = Console.ReadLine();

                        //Text
                        Console.WriteLine("\n\tAnge text:");
                        bloggen[index][2] = Console.ReadLine();

                        // Meddelande till användaren... 
                        Console.WriteLine("\n\tInlägg ändrat!");

                        return false; // När inlägget är redigerat så slutar vi visa menyn. 
                    }
                    return true;
                    
                
                // Radera
                case 2:
                    // Ber användaren om input och sparar det i index variabeln. 
                    Console.WriteLine("\n\tVälj index att radera:");
                    Int32.TryParse(Console.ReadLine(), out index);

                    // Kontrollpunkt - möjlighet att avbryta raderingen. 
                    Console.WriteLine("\n\tSka inlägget verkligen avbrytas?\n\tange y för att fortsätta."); 
                    if(Console.ReadLine().ToLower() == "y")
                    {
                        // om y väljs tömmer vi skärmen, kallar på metoden för att radera, ger användaren ett meddelande och avslutar undermenyn.
                        Console.Clear();
                        bloggen.RemoveAt(index);
                        Console.WriteLine("\n\tInlägget borttaget!");
                        return false; 
                    }
                    else
                    {
                        Console.WriteLine("\n\tAvbrytet av anvädare!");
                        return true; // fortsätter visa menyn, användaren kanske klickade fel och skulle radera istället för redigera. Då är det fint om vi är i menyn. 
                    }
           
                // Avbryt
                case 3:
                    Console.Clear();
                    return false;
                // Om felaktigt värde i menyn väljs visar vi bara menyn igen. 
                default:
                    return true;
            }
        }


        /* ***************************************************************************
         * UTSKRIFT AV INLÄGG. 
         * Tar emot listan, skriver ut listan. Inget retuneras. 
         *****************************************************************************/
        static void listaInlägg(List<string[]> bloggen)
        {
            Console.WriteLine("\tHär är alla blogginläggen:");
            // Loopar igenom alla inlägg och presenterar dem. 
            foreach (string[] element in bloggen)
            {
                Console.WriteLine("\t******************************************");
                Console.Write("\t" + element[1] + "(" + element[0] + ")\n\t" + element[2] + "\n");
                Console.WriteLine("\t******************************************");
            }
        }


        /* ***************************************************************************
         * SORTERING    
         * Tar emot listan och sorterar den. Inget retuneras. 
         *****************************************************************************/
        static void sorteraBloggen(List<string[]> bloggen)
        {
            int maxListan = bloggen.Count - 1; // Antalet element som vi ska loopa igenom 

            for (int i = 0; i < maxListan; i++) // Yttre loop.
            {
                int leftOfList = maxListan - i; // Minskar storleken på loopen.

                for (int j = 0; j < leftOfList; j++) // Inre loop.
                {

                    int tmp = bloggen[j][1].CompareTo(bloggen[j + 1][1]); // Jämför nuvarande element mot nästkommande element. 
                    if (tmp > 0)
                    {
                        // Själva bytet i sorteringen.                      
                        // Håller ordLista nuvarnade element temporärt. 
                        string tmp2 = bloggen[j][1];
                        string tmp3 = bloggen[j][0];
                        string tmp4 = bloggen[j][2];

                        // sätter nuvarande element till nästkommande element. 
                        bloggen[j][1] = bloggen[j + 1][1];
                        bloggen[j][0] = bloggen[j + 1][0];
                        bloggen[j][2] = bloggen[j + 1][2];

                        // Sätter nästkommande element till nuvarande element.
                        bloggen[j + 1][1] = tmp2;
                        bloggen[j + 1][0] = tmp3;
                        bloggen[j + 1][2] = tmp4;
                    }
                }
            }
        }

        /* ***************************************************************************
         * LINJÄR SÖKNING AV TITEL 
         * Tar emot listan och ett sökvärde, Skriver ut eventuella träffar av sökningen
         * en bool retuneras för att hålla koll på ifall vi fick sökträff, för att 
         * veta om vi ska ladda en ny meny. 
         *****************************************************************************/
        static bool SökTitel(List<string[]> bloggen, string sökVärde)
        {
            bool hittat = false;

            Console.WriteLine("\n\tSök resultat:");

            // Eftersom jag behöver ha indexet till att redigera eller radera ett inlägg kör vi en for loop istället för foreach. 
            for (int i = 0; i < bloggen.Count; i++)
            {
                if (bloggen[i][1].ToLower() == sökVärde.ToLower())
                {
                    Console.WriteLine("\tIndex:" + i + " Datum:" + bloggen[i][0] + "Titel:" + bloggen[i][1] + "Inlägg:" + bloggen[i][2]);
                    hittat = true;

                }
            }

            // Om boolen inte har satts till sann så vet vi att inget hittats så vi skriver ut ett meddelande till användaren. 
            if (!hittat)
            {
                Console.WriteLine("\tSökordet <" + sökVärde.ToLower() + "> kunde inte hittas.");
                hittat = false;
            }
            return hittat;
        }


        /* ***************************************************************************
         * BINÄR SÖKNING AV TITELENS FÖRSTA BOKSTAV
         * Tar emot listan och ett sökvärde, Skriver ut första träffen av den sorterade 
         * listan. En bool retuneras för att hålla koll på ifall vi fick sökträff, 
         * för att veta om vi ska ladda en ny meny. 
         *****************************************************************************/
        static bool binärSökTitel(List<string[]> bloggen, string sökvärde)
        {
            bool hittat = false;
            int första = 0; // första elementet. 
            int sista = bloggen.Count - 1; // sista elementet. 

            while (första <= sista)  // loopar igenom listan så länge först är mindre eller likamed sista. 
            {
                int mellan = (första + sista) / 2; // Mellan. "första" + "sista" delat med 2. 
                int tmp = sökvärde[0].CompareTo(bloggen[mellan][1][0]); // Hämtar ut första bokstaven i <sökvärde> och jämför mot första bokstaven i andra elementet <mitten>. 

                //  OM sökvärde är större än siffra på indexplats "mellan" i bloggen. "första" får värdet av "mellan" + 1
                if (tmp > 0)
                {
                    första = mellan + 1;
                }

                //ANNARS OM sökvärde är mindre än siffra på indexplats "mellan" i bloggen. "sista" får värdet av "mellan" - 1
                else if (tmp < 0)
                {
                    sista = mellan - 1;
                }
                // Skriv ut att siffran de sökt på finns på element "mellan" i bloggen.
                else
                {
                    Console.WriteLine("\n\tSökvärdet: " + sökvärde[0] + " finns på element: " + mellan + ".");
                    hittat = true; // Sökresultat hittat. 
                    return hittat;
                    
                }
            }

            // OM "första" är större än "sista" när sökningen är över. Skriv ut att sökningen inte lyckades.
            if (första > sista)
            {
                Console.WriteLine("\n\tSökvärdet: " + sökvärde + " hittades inte. Sökningen misslyckades.");
                hittat = false; // sätter boolen till falskt då inget hittades. 
            }

            return hittat;
         }


        static void Main(string[] args)
        {
            bool visaMeny = true; // Huvudmenyn. 
            bool visaUnderMeny, inputKontroll; // Undermenyn och en kontroll. 
            bool ärSorterad = false; // Håller koll på ifall listan är sorterad. 

            List<string[]> bloggen = new List<string[]>();  // Listan som håller alla blogginlägg. 
            List<string[]> dummy = new List<string[]>(); // En dummy lista som kan användas för att skicka till metoden inputKontrollMetod. 

            // Laddar menyn. 
            while (visaMeny)
            {
                // Menyvalen
                Console.WriteLine("\n\tVälkommen till bloggen!" +
                    "\n\t[1]Skriv ut alla blogginlägg" +
                    "\n\t[2]Skriv ett nytt inlägg" +
                    "\n\t[3]Sök ett inlägg i bloggen" +
                    "\n\t[4]Binär sökning av inlägg i bloggen" +
                    "\n\t[5]Avsluta");

                Int32.TryParse(Console.ReadLine(), out int meny); // input från menyn. 

                // Hanterar inputen för menyn. 
                switch (meny)
                {
                    // Skriv ut alla poster i bloggen
                    case 1:
                        Console.Clear(); // Tömmer skärmen för lättare läsning.
                        listaInlägg(bloggen); // Anropar metoden med listan som in värde. 
                        break;

                    // Skapa ett inlägg
                    case 2:
                        Console.Clear();// Tömmer skärmen för lättare läsning.

                        string[] addToBlogg = new string[3]; // Skapa en ny vektor med 3 element. 1=Datum, 2=Titel, 3=Texten. 
                        string date = DateTime.Now.ToString("yyyy-MM-dd"); // Skapar en ny sträng med dagens datum. 

                        // Adderar datum samt värde från användaren. 
                        addToBlogg[0] = date;


                        
                        inputKontroll = false; // Sätter inputKontroll till falsk så vi vet att inputet inte klarat valideringen. 

                        while (!inputKontroll)
                        {
                            // Frågar användaren infter input och sparar i en sträng. 
                            Console.WriteLine("\n\tAnge titel");
                            string input = Console.ReadLine();

                            inputKontroll = inputKontrollMetod(input);// Anropat metoden för att validera inputen. 

                            // Om valideringen klaras, adderar vi det till bloggen.
                            if (inputKontroll)
                            {
                                addToBlogg[1] = input;
                            }                            
                        }

                        
                        inputKontroll = false; // Sätter inputKontroll till falsk så vi vet att inputet inte klarat valideringen. 

                        // Gör en kontroll mot tomt inlägg. 
                        while (!inputKontroll)
                        {
                            // Frågar användaren infter input och sparar i en sträng. 
                            Console.WriteLine("\n\tSkriv ditt inlägg");
                            string input = Console.ReadLine();

                            inputKontroll = inputKontrollMetod(input); // Anropat metoden för att validera inputen. 
                            
                            // Om valideringen klaras, adderar vi det till bloggen.
                            if (inputKontroll)
                            {
                                addToBlogg[2] = input;
                            }

                        }                                                                      
                        

                        bloggen.Add(addToBlogg); // Anropar metoden som adderat ett inlägg i bloggen, tar vektorn som parameter.  

                        Console.WriteLine("\tInlägget sparat!\n");

                        break;

                    // Linjär sökning.
                    case 3:                        
                        Console.Clear();
                        if (bloggen.Count > 0) // Kontrollerar så där finns något i listan. 
                        {
                            
                            inputKontroll = false; // Sätter inputKontroll till falsk så vi vet att inputet inte klarat valideringen. 

                            // Gör en kontroll mot sökningen. 
                            while (!inputKontroll)
                            {
                                // Frågar användaren infter input och sparar i en sträng. 
                                Console.WriteLine("\n\tAnge en titel att söka efter:");
                                string sökVärde = Console.ReadLine();

                                inputKontroll = inputKontrollMetod(sökVärde); // Anropat metoden för att validera inputen. 

                                // Om valideringen klaras, adderar vi det till bloggen.
                                if (inputKontroll)
                                {
                                    // Anropar metoden för sökning. Tar bloggen och sökvärde från användaren som parameter. Om true retuneras laddar vi menyn. 
                                    if (SökTitel(bloggen, sökVärde))
                                    {
                                        visaUnderMeny = true; // menyn ska visas. 
                                        while (visaUnderMeny)
                                        {
                                            visaUnderMeny = underMeny(bloggen); // Anropar undermenyn. underMeny() retunerar en bool, så vi kan avbryta menyn vid behov.
                                        }
                                    }
                                }

                            }
                        }
                        else
                        {
                            Console.WriteLine("\n\tBloggan har inga inlägg ännu.\n");
                        }

                        break;
                    
                    // Binär sökning
                    case 4:
                        Console.Clear();
                        if (bloggen.Count > 0) // Kontrollerar så där finns något i listan. 
                        {
                            // Om listan inte är sorterad så sorterar vi den, ett krav för att den binära sökningen ska kunna genomföras.
                            if (!ärSorterad) sorteraBloggen(bloggen); 

                            // Ber användaren om sökvärde.
                            Console.WriteLine("\tAnge första bokstaven i titeln du vill söka efter:");
                            string sökVärde = Console.ReadLine();

                            // Kontrollerar så där finns något att söka efter, så inte användaren bara tryckte "Enter".
                            if (sökVärde.Length <= 0)
                            {
                                Console.WriteLine("\n\tDu måste ange ett värde att söka efter"); // Vid binär sökning måste vi ha ett värde med oss in. 
                            }
                            else
                            {
                                // Anropar metoden för sökning. Tar bloggen och sökvärde från användaren som parameter. Om true retuneras laddar vi menyn. 
                                if (binärSökTitel(bloggen, sökVärde))
                                {
                                    visaUnderMeny = true; // menyn ska visas. 
                                    while (visaUnderMeny)
                                    {
                                        visaUnderMeny = underMeny(bloggen); // Anropar undermenyn. underMeny() retunerar en bool, så vi kan avbryta menyn vid behov.
                                    }
                                }
                            }
                                                  
                        }
                        else
                        {
                            Console.WriteLine("\n\tBloggan har inga inlägg ännu.\n"); // Om där inte finns några inlägg visar vi detta meddelandet till användaren. 
                        }
                        break;
                    
                    // avslutar programmet
                    case 5:
                        visaMeny = false;
                        break;
                }
            }
        }
    }
}
