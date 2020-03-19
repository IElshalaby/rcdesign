from tkinter import *
from tkinter import messagebox
import math
from decimal import *
root = Tk()
root.title("R.C. Design v0.2 [BETA]")
#root.iconbitmap(r"C:\Users\YN\Documents\Concrete Design\Main_icon.ico")
root.resizable(0, 0)
# root.geometry("430x350")
l1 = Label(root, text="b {mm}")
l2 = Label(root, text="d {mm}")
l3 = Label(root, text="Fcu {N/mm^2}")
l4 = Label(root, text="Max Shear Value (Qumax) {KN}")
l11 = Label(root, text="Max Moment (Mu) {KN.m}")
l5 = Label(root, text="")
l5_2 = Label(root, text="")
l51 = Label(root, text="")
l6 = Label(root, text="qact=........")
l7 = Label(root, text="qst=..........")
res2 = Label(root, text="")
res4 = Label(root, text="")
lor = Label(root, text=" or")
x1 = Entry(root)
x2 = Entry(root)
x3 = Entry(root)
x11 = Entry(root)
x4 = Entry(root)
l1.grid(row=0, column=0)
x1.grid(row=0, column=2)
l2.grid(row=1, column=0)
x2.grid(row=1, column=2)
l3.grid(row=2, column=0)
x3.grid(row=2, column=2)
l11.grid(row=3, column=0)
x11.grid(row=3, column=2)
l4.grid(row=4, column=0)
x4.grid(row=4, column=2)
i = IntVar()


def about():
    abo1 = "This is a free software license and is compatible with the GNU GPL.\nSoftware was developed by/ Ibrahim A. Elshalaby"
    messagebox.showinfo(title='about', message=abo1)


def change():
    chng1 = """-v0.0 Beta:
     *Software released.
     *Added:
       -You can solve shear only with an 8mm diameter steel.
--------------------------------------------------------------------------------
-v0.1 Beta:
    *Added:
       -Introducing GUI !
       -README file is now available to instruct you.
       -You can solve moment only with a 16mm diameter steel.
       -You can solve shear with a 10mm diameter steel also, but            the software decides which diameter to use.
--------------------------------------------------------------------------------
-v0.2 Beta:
    *Added:
       -You can choose a diameter from 16 and 18 when solving            for moment.
       -About and Changelog buttons added for more info                    about the software.
    *Changed:
       -Changed the GUI's layout.
       -Changed the icon to a more elegant one.
       -No more recommendations for the secondary steel                      diameter.
    *Removed:
       -No need for a README file anymore.
    *Fixed:
       -Fixed a bug with results using \'or\' when solving for                    shear."""
    messagebox.showinfo(title='Changelog', message=chng1)


def moment():
    b = float(x1.get())
    d = float(x2.get())
    Fcu = float(x3.get())
    Mu = abs(float(x11.get()))
    if b <= 0 or d <= 0 or Fcu <= 0 or Mu <= 0:
        messagebox.showerror(title="Error", message="Unexpected value!")
    aos = i.get()
    if aos == 201:
        dos = 16
    if aos == 255:
        dos = 18
    ku = Decimal((Mu*10**6) / (b*d**2))
    Ku = round(ku, 1)
    if Fcu == 20:
        um_table = {'0.8': '27', '0.9': '30', '1.0': '34', '1.1': '38', '1.2': '41', '1.3': '45', '1.4': '49', '1.5': '53', '1.6': '57', '1.7': '61', '1.8': '65', '1.9': '69', '2.0': '73', '2.1': '78', '2.2': '82', '2.3': '87', '2.4': '91', '2.5': '96', '2.6': '100', '2.7': '104', '2.8': '107', '2.9': '110',
                    '3.0': '114', '3.1': '117', '3.2': '120', '3.3': '124', '3.4': '127', '3.5': '130', '3.6': '134', '3.7': '137', '3.8': '141', '3.9': '144', '4.0': '147', '4.1': '151', '4.2': '154', '4.3': '157', '4.4': '161', '4.5': '164', '4.6': '167', '4.7': '171', '4.8': '174', '4.9': '177', '5.0': '181'}
        us_table = {'2.7': '4', '2.8': '7', '2.9': '10', '3.0': '14', '3.1': '17', '3.2': '20', '3.3': '24', '3.4': '27', '3.5': '30', '3.6': '34', '3.7': '37', '3.8': '41', '3.9': '44', '4.0': '47', '4.1': '51',
                    '4.2': '54', '4.3': '57', '4.4': '61', '4.5': '64', '4.6': '67', '4.7': '71', '4.8': '74', '4.9': '77', '5.0': '81'}
        ss = 2.7
    if Fcu == 22.5:
        um_table = {'0.8': '27', '0.9': '30', '1.0': '34', '1.1': '37', '1.2': '41', '1.3': '45', '1.4': '48', '1.5': '52', '1.6': '56', '1.7': '60', '1.8': '64', '1.9': '68', '2.0': '72', '2.1': '76', '2.2': '80', '2.3': '85', '2.4': '89', '2.5': '94', '2.6': '98', '2.7': '103', '2.8': '108', '2.9': '112',
                    '3.0': '115', '3.1': '119', '3.2': '122', '3.3': '125', '3.4': '129', '3.5': '132', '3.6': '135', '3.7': '139', '3.8': '142', '3.9': '145', '4.0': '149', '4.1': '152', '4.2': '156', '4.3': '159', '4.4': '162', '4.5': '166', '4.6': '169', '4.7': '172', '4.8': '176', '4.9': '179', '5.0': '182'}
        us_table = {'3.0': '3', '3.1': '6', '3.2': '9', '3.3': '13', '3.4': '16', '3.5': '20', '3.6': '23', '3.7': '26', '3.8': '30', '3.9': '33', '4.0': '36', '4.1': '40',
                    '4.2': '43', '4.3': '46', '4.4': '50', '4.5': '53', '4.6': '56', '4.7': '60', '4.8': '63', '4.9': '67', '5.0': '70'}
        ss = 3.0
    if Fcu == 25:
        um_table = {'0.8': '27', '0.9': '30', '1.0': '34', '1.1': '37', '1.2': '41', '1.3': '44', '1.4': '48', '1.5': '52', '1.6': '56', '1.7': '59', '1.8': '63', '1.9': '67', '2.0': '71', '2.1': '75', '2.2': '79', '2.3': '83', '2.4': '88', '2.5': '92', '2.6': '96', '2.7': '101', '2.8': '105', '2.9': '110',
                    '3.0': '114', '3.1': '119', '3.2': '124', '3.3': '127', '3.4': '130', '3.5': '134', '3.6': '137', '3.7': '140', '3.8': '144', '3.9': '147', '4.0': '150', '4.1': '154', '4.2': '157', '4.3': '161', '4.4': '164', '4.5': '167', '4.6': '171', '4.7': '174', '4.8': '177', '4.9': '181', '5.0': '184'}
        us_table = {'3.3': '2', '3.4': '5', '3.5': '9', '3.6': '12', '3.7': '15', '3.8': '19', '3.9': '22', '4.0': '25', '4.1': '29',
                    '4.2': '32', '4.3': '36', '4.4': '39', '4.5': '42', '4.6': '46', '4.7': '49', '4.8': '52', '4.9': '56', '5.0': '59'}
        ss = 3.3
    if Fcu == 27.5:
        um_table = {'0.8': '27', '0.9': '30', '1.0': '34', '1.1': '37', '1.2': '41', '1.3': '44', '1.4': '48', '1.5': '51', '1.6': '55', '1.7': '59', '1.8': '63', '1.9': '66', '2.0': '70', '2.1': '74', '2.2': '78', '2.3': '82', '2.4': '86', '2.5': '90', '2.6': '95', '2.7': '99', '2.8': '103', '2.9': '108',
                    '3.0': '112', '3.1': '116', '3.2': '121', '3.3': '126', '3.4': '130', '3.5': '135', '3.6': '139', '3.7': '142', '3.8': '145', '3.9': '149', '4.0': '152', '4.1': '155', '4.2': '159', '4.3': '162', '4.4': '165', '4.5': '169', '4.6': '172', '4.7': '176', '4.8': '179', '4.9': '182', '5.0': '186'}
        us_table = {'3.6': '1', '3.7': '4', '3.8': '8', '3.9': '11', '4.0': '15', '4.1': '18',
                    '4.2': '21', '4.3': '25', '4.4': '28', '4.5': '31', '4.6': '35', '4.7': '38', '4.8': '41', '4.9': '45', '5.0': '48'}
        ss = 3.6
    if Fcu == 30:
        um_table = {'0.8': '27', '0.9': '30', '1.0': '33', '1.1': '37', '1.2': '40', '1.3': '44', '1.4': '48', '1.5': '51', '1.6': '55', '1.7': '58', '1.8': '62', '1.9': '66', '2.0': '70', '2.1': '74', '2.2': '77', '2.3': '81', '2.4': '85', '2.5': '89', '2.6': '93', '2.7': '98', '2.8': '102', '2.9': '106',
                    '3.0': '110', '3.1': '115', '3.2': '119', '3.3': '123', '3.4': '128', '3.5': '132', '3.6': '137', '3.7': '142', '3.8': '147', '3.9': '150', '4.0': '154', '4.1': '157', '4.2': '160', '4.3': '164', '4.4': '167', '4.5': '170', '4.6': '174', '4.7': '177', '4.8': '181', '4.9': '184', '5.0': '187'}
        us_table = {'4.0': '4', '4.1': '7', '4.2': '10', '4.3': '14', '4.4': '17',
                    '4.5': '20', '4.6': '24', '4.7': '27', '4.8': '31', '4.9': '34', '5.0': '37'}
        ss = 4.0
    if Ku < 0.8:
        um = float(um_table['0.8'])
        um = um/10000
    elif Ku >= 0.8 and Ku <= 5.0:
        um = float(um_table[str(Ku)])
        um = float(um)/10000
    elif Ku > 5.0:
        um = float(um_table['5.0'])
        um = um/10000
    Asm = (1.1/360)*b*d
    As_m = um*b*d
    if As_m < Asm:
        As_m = Asm
    n_m = math.ceil(As_m / aos)
    if Ku < ss:
        us = 0.0
    elif Ku >= ss and Ku <= 5.0:
        us = float(us_table[str(Ku)])
        us = us/10000
    elif Ku > 5.0:
        us = float(us_table['5.0'])
        us = us/10000
    As_s = us*b*d
    if us == 0.0:
        m11 = "Ku= " + str(Ku) + "\n" + "μ= " + str(round(um*100, 1)) + "\n" + "μ\'= " + str(round(us*100, 1)) + "\n" + "As= " + str(round(As_m, 2)) + \
            "\n" + "As\'= " + str(round(As_s, 2)) + "\n" + \
            "Main Reinforcment: " + \
            str(n_m) + "ø" + str(dos) + "\n" + \
            "No secondary reinforcment needed."
    else:
        m11 = "Ku= " + str(Ku) + "\n" + "μ= " + str(round(um*100, 1)) + "\n" + "μ\'= " + str(round(us*100, 1)) + "\n" + "As= " + str(round(As_m, 2)) + \
            "\n" + "As\'= " + str(round(As_s, 2)) + "\n" + \
            "Main Reinforcment: " + str(n_m) + "ø" + str(dos)
    messagebox.showinfo(title='Solution', message=m11)


def shear():
    res2.pack_forget()
    res4.pack_forget()
    lor.pack_forget()
    b = float(x1.get())
    d = float(x2.get())
    Fcu = float(x3.get())
    Qu_max = abs(float(x4.get()))
    if b <= 0 or d <= 0 or Fcu <= 0 or Qu_max <= 0:
        messagebox.showerror(title="Error", message="Unexpected value!")
    qact = (Qu_max*1000)/(b*d)
    q_uncracked = 0.16*(Fcu/1.5)**0.5
    q_cracked = 0.12*(Fcu/1.5)**0.5
    q_max = 0.7*(Fcu/1.5)**0.5
    l6.config(text="qact= " + str(round(qact, 3)))

    def couldnt():
        """stirrups won\'t be able to resist this shear value"""
        messagebox.showinfo(
            title="Result", message="Use bent bars or Increase Dimensions")

    def design():
        x = 10
        As = 78.5
        Fy = 360
        _8_10_2(x, As, Fy)

    def _8_10_2(x, As, Fy):
        l6.grid(row=10, column=1)
        l7.grid(row=11, column=1)
        S = (2*As*(Fy/1.15))/(b*qst)
        n = math.ceil(1000/S)
        if n < 5:
            z2 = "Use minimum stirrups: 5" + "∅" + str(x) + "/m\'"
            res2.config(text=z2)
            res2.grid(row=12, column=1)
        if n >= 5 and n <= 10:
            z2 = "Use " + str(n) + "∅" + str(x) + "/m\'"
            res2.config(text=z2)
            res2.grid(row=12, column=1)
        if n > 10:
            _8_10_4(x, As, Fy)

    def _8_10_4(x, As, Fy):
        S = (4*As*(Fy/1.15))/(b*qst)
        n = math.ceil(1000/S)
        if n < 5:
            z4 = "Use minimum stirrups: 5" + "∅" + \
                str(x) + "/m\'" + " {4 branches}"
            res4.config(text=z4)
            res4.grid(row=14, column=1)
            if x == 8:
                lor.grid(row=13, column=1)
                design()
        if n >= 5 and n <= 10:
            z4 = "Use " + str(n) + "∅" + str(x) + "/m\'" + " {4 branches}"
            res4.config(text=z4)
            res4.grid(row=14, column=1)
            if x == 8:
                lor.grid(row=13, column=1)
                design()
        if n > 10:
            if x == 10:
                couldnt()
            design()

    if qact > q_max:
        messagebox.showinfo(
            title="Result", message="qact > qmax, Increase Dimensions")
    if qact < q_uncracked:
        res2.config(text="Use minimum stirrups: 5∅8/m\'")
    if qact > q_uncracked and qact < q_max:
        qst = qact - q_cracked
        l7.config(text="qst= " + str(round(qst, 3)))
        x = 8
        As = 50.8
        Fy = 240
        _8_10_2(x, As, Fy)


rb16 = Radiobutton(root, text="ø16", value=201, variable=i)
rb18 = Radiobutton(root, text="ø18", value=255, variable=i)
moment_btn = Button(root, text="Solve for Moment", command=moment)
l51.grid(row=5)
rb16.grid(row=6, column=0)
rb18.grid(row=6, column=1)
moment_btn.grid(row=6, column=2)
shear_btn = Button(root, text="Solve for Shear", command=shear)
l5.grid(row=7)
shear_btn.grid(row=8, column=1)
l5_2.grid(row=9)
ab_btn = Button(root, text='About', command=about)
cl_btn = Button(root, text='Changelog', command=change)
ab_btn.grid(row=15, column=0)
cl_btn.grid(row=15, column=2)
root.mainloop()
