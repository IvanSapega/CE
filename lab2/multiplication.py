from BitVector import BitVector

try:
    print("Введіть перший множник:", end="")
    multiplicand = BitVector(intVal=int(input(), 2), size=32)
    print("Введіть другий множник:", end="")
    multiplier = BitVector(intVal=int(input(), 2), size=32)
    multiplication(multiplicand, multiplier)
except Exception as e:
    print(e.__str__())

def multiplication(multiplicand, multiplier):
    carry = 0
    register = BitVector(intVal=multiplier.int_val(), size=64)
    print("Початковий регістр:", register)
    for i in range(30, -1, -1):
        if register[-1]:
            for y in range(31, -1, -1):
                if carry:
                    if register[y + 1] & multiplicand[y]:
                        register[y + 1] = 1
                        carry = 1
                    elif register[y + 1] | multiplicand[y]:
                        register[y + 1] = 0
                        carry = 1
                    else:
                        register[y + 1] = 1
                        carry = 0
                else:
                    if register[y + 1] & multiplicand[y]:
                        register[y + 1] = 0
                        carry = 1
                    elif register[y + 1] | multiplicand[y]:
                        register[y + 1] = 1
                    else:
                        register[y + 1] = 0
            print("Додавання першого множника в регістр:", register)
        print("Здвиг регістру на один:", register)
        register.shift_right_by_one()
    print("Результат:", register)
    print("Результат у десятковому:", register.int_val())