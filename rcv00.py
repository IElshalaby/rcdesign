import math
b = float(input("b? {mm}\n"))
d = float(input("d? {mm}\n"))
Fcu = float(input("Fcu? {N/mm^2}\n"))
Qu_max = abs(float(input("Enter the maximum shear value? {KN}\n")))
if b <= 0 or d <= 0 or Fcu <= 0 or Qu_max <= 0:
    print("Unexpected Value")
qact = (Qu_max*1000)/(b*d)
print("Results:\n" + "qact=" + str(qact))
q_uncracked = 0.16*(Fcu/1.5)**0.5
q_cracked = 0.12*(Fcu/1.5)**0.5
q_max = 0.7*(Fcu/1.5)**0.5


def couldnt():
    """stirrups won\'t be able to resist this shear value"""
    print("Use bent bars or Increase Dimensions")


def design():
    x = 10
    As = 78.5
    Fy = 360
    _8_10_2(x, As, Fy)


def _8_10_2(x, As, Fy):
    S = (2*As*(Fy/1.15))/(b*qst)
    n = math.ceil(1000/S)
    if n < 5:
        print("Use minimum stirrups: 5" + "∅" + str(x) + "/m\'")
    if n >= 5 and n <= 10:
        print("Use " + str(n) + "∅" + str(x) + "/m\'")
    if n > 10:
        _8_10_4(x, As, Fy)


def _8_10_4(x, As, Fy):
    S = (4*As*(Fy/1.15))/(b*qst)
    n = math.ceil(1000/S)
    if n < 5:
        print("Use minimum stirrups: 5" + "∅" +
              str(x) + "/m\'" + " {4 branches}")
        if x == 8:
            print("or")
            design()
    if n >= 5 and n <= 10:
        print("Use " + str(n) + "∅" + str(x) + "/m\'" + " {4 branches}")
        if x == 8:
            print("or")
            design()
    if n > 10:
        if x == 10:
            couldnt()
        design()


if qact > q_max:
    print("qact > qmax, Increase Dimensions")
if qact < q_uncracked:
    print("Use minimum stirrups: 5∅8/m\'")
if qact > q_uncracked and qact < q_max:
    qst = qact - q_cracked
    print("qst=" + str(qst))
    x = 8
    As = 50.8
    Fy = 240
    _8_10_2(x, As, Fy)
