using Esercitazione_3Settimana.GestionaleSpese;

Console.WriteLine("Benvenuto nel gestionale spese sceli un operazione");
bool verify = false;
int scelta = -1;
do
{
    Console.WriteLine(
        "1 - Inserisci una nuova spesa.\n" +
        "2 - Approva una spesa. \n" +
        "3 - Cancella una spesa. \n" +
        "4 - Mostra elenco spese approvate. \n" +
        "5 - Mostra elenco delle spese di un utente. \n" +
        "6 - Mostra elenco totale delle spese per categoria. \n" +
        "0 - Esci.\n");
    if (int.TryParse(Console.ReadLine(), out scelta))
    {
        verify = true;
    }
} while (!verify && scelta >= 0 && scelta <= 6);
switch (scelta)
{
    case 0:
        Console.WriteLine("Grazie e arrivederci");
        break;
    case 1:
        InsertNewSpesa();//Completato con ConnectedMode
        break;
    case 2:
        ApprovaSpesa();//Copletato con ConnectedMode
        break;
    case 3:
        DeleteSpesa();//Completato con ConnectedMode - Errore con ADODisconnectedMode.DeleteSpesaById(int idSpesa)
        break;
    case 4:
        GetAllApprovate(); //Completato con ConnectedMode
        break;
    case 5:
        GetAllSpeseByUtente(); //Completato con ConnectedMode
        break;
    case 6:
        GetAllByCategoria();
        break;
}

void InsertNewSpesa()
{
    int idCat;
    DateTime dos;
    int importo;
    do
    {
        Console.WriteLine("Inserisci la categoria tra quelle presenti \n");
        ADOConnectedMode.GetAllCategoria();

    } while (!int.TryParse(Console.ReadLine(), out idCat));

    do
    {
        Console.WriteLine("Inserisci la data di quando è stata effettuata la spesa\n");

    } while (!DateTime.TryParse(Console.ReadLine(), out dos));
    Console.WriteLine("Inserisci una descrizione");
    string desc = Console.ReadLine();
    Console.WriteLine("Inserisci il nome Utente");
    string utente = Console.ReadLine();
    do
    {
        Console.WriteLine("Inserisci l'importo della tua spesa\n");

    } while (!int.TryParse(Console.ReadLine(), out importo));
    ADOConnectedMode.Insert(idCat, dos, desc, utente, importo);

}

void ApprovaSpesa()
{
    int idSpesa;
    do
    {
        Console.WriteLine("Inserisci l'id della spesa da approvare tra quelle presenti \n");
        ADOConnectedMode.GetAllSpesaNonApprovate();

    } while (!int.TryParse(Console.ReadLine(), out idSpesa));
    ADOConnectedMode.UpdateSpesaById(idSpesa);

}

void DeleteSpesa()
{
    int idSpesa;
    do
    {
        Console.WriteLine("Inserisci l'id della spesa che desideri eliminare \n");
        ADOConnectedMode.GetAllSpesaNonApprovate();

    } while (!int.TryParse(Console.ReadLine(), out idSpesa));
    ADOConnectedMode.DeleteSpesaById(idSpesa);
}

void GetAllApprovate()
{
    ADOConnectedMode.GetAllApproved();
}

void GetAllSpeseByUtente()
{
   Console.WriteLine("Inserisci il nome dell'Utente per il quale vuoi visualizzare le spese");
    ADOConnectedMode.GetAllUtenti();
    string utente = Console.ReadLine();
    ADOConnectedMode.GetAllSpeseByUtente(utente);
}

void GetAllByCategoria()
{
    ADOConnectedMode.GetAllSpeseByCat();
}