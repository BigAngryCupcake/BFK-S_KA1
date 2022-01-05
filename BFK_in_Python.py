
def goto_zuviel():
    file.close()
    print("Fehler!, Es sind zuviele Monate vorhanden, überprüfen Sie bitte das Textdokument")


def goto_wenig():
    file.close()
    print("Fehler!, Es sind zuwenige Monate vorhanden, überprüfen Sie bitte das Textdokument")

geld_alles = 0
zahl = 0
Ende = True


file = open("Ums2020Berlin.txt", "r")
file1 = open("Ums2020Berlin.txt", "r")


ausgelesen = file.readline().split("#")
anfang = ausgelesen[0]
anfang = anfang.replace(",", ".")


anfangs_geld = float(anfang)


min_geld = anfangs_geld
max_geld = anfangs_geld



while Ende:

    line = file.readline()
    #print(line)

    for zeile in file1:
        
        erste_linie = zeile.split("#")
        zerhakt = erste_linie[0]
        zerhakt = zerhakt.replace(",", ".")

        geld = float(zerhakt)

       # print(geld)
        if min_geld > geld:
            min_geld = geld


        if max_geld < geld:
            max_geld = geld

        geld_alles = geld_alles + geld

        zahl = zahl + 1


    if not line:
      Ende = False
      
#print(zahl)

if zahl < 12:
    goto_wenig()
        

if zahl > 12:
    goto_zuviel()


if zahl == 12:
   print("Der Jahresumsatz beträgt:")
   umsatz = geld_alles / zahl
   print(umsatz)



file.close()