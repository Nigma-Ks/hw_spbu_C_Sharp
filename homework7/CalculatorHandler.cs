using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace homework7
{
    public class Calculator
    {

        enum States
        {
            waitingForFirstOperand,
            operatorEntered,
            equalitySignEntered,
            error
        }

        public string accOperand;
        string currOperand;
        private char? operation;
        States _currState;

        private double? acc;
        public string expression;
        public Calculator()
        {
            _currState = States.waitingForFirstOperand;
            accOperand = "";
            expression = "";
            acc = null;
        }

        public void CalculationProcess(string character)
        {
            switch (_currState)
            {
                case States.waitingForFirstOperand:

                    if (character[0] >= '0' && character[0] <= '9')
                    {
                        accOperand += character;
                        expression += character;

                    }
                    else if (character[0] == ',' || character[0] == '.')
                    {
                        if (accOperand == "")
                        {
                            _currState = States.error;
                            break;
                        }
                        accOperand += character;
                        expression += character;

                    }
                    else if (character.Equals("-") || character.Equals("+") || character.Equals("/")
                            || character.Equals("*"))
                    {
                        if (accOperand == "")
                        {
                            _currState = States.error;
                            break;
                        }
                        _currState = States.operatorEntered;
                        operation = character[0];
                        expression += character;
                    }
                    else if (character.Equals("="))
                    {
                        if (accOperand == "")
                        {
                            _currState = States.error;
                            break;
                        }
                        _currState = States.equalitySignEntered;

                    }
                    else if (character.Equals("AC"))
                    {
                        expression = "";
                        accOperand = "";
                    }
                    break;

                case States.error:
                    if (character == "AC")
                    {
                        expression = "";
                        acc = null;
                        accOperand = "";
                        currOperand = "";
                        _currState = States.waitingForFirstOperand;
                    }
                    else
                    {
                        expression = "";
                        accOperand = "error!";
                        acc = null;
                        currOperand = "";
                        _currState = States.error;
                    }
                    break;
                case States.operatorEntered:
                    if (character[0] >= '0' && character[0] <= '9')
                    {
                        currOperand += character;
                        expression += character;

                    }
                    else if (character[0] == ',' || character[0] == '.')
                    {
                        if (currOperand.Length == 0)
                        {
                            _currState = States.error;
                            break;
                        }
                        currOperand += character;
                        expression += character;

                    }
                    else if (character.Equals("-") || character.Equals("+") || character.Equals("/")
                            || character.Equals("*") || character.Equals("="))
                    {
                        if (currOperand == "" || currOperand == null)
                        {
                            _currState = States.error;
                        }
                        else
                        {
                            switch (operation)
                            {
                                case '+':
                                    if (acc == null) acc = Double.Parse(accOperand) + Double.Parse(currOperand);
                                    else acc += Double.Parse(currOperand);
                                    _currState = States.operatorEntered;
                                    currOperand = "";
                                    break;
                                case '-':
                                    if (acc == null) acc = Double.Parse(accOperand) - Double.Parse(currOperand);
                                    else acc -= Double.Parse(currOperand);
                                    _currState = States.operatorEntered;
                                    currOperand = "";
                                    break;
                                case '*':
                                    if (acc == null) acc = Double.Parse(accOperand) * Double.Parse(currOperand);
                                    else acc *= Double.Parse(currOperand);
                                    _currState = States.operatorEntered;
                                    currOperand = "";
                                    break;
                                case '/':
                                    if (Double.Parse(currOperand) < 0.01)
                                    {
                                        _currState = States.error;
                                        accOperand = "error!";
                                        break;
                                    }
                                    if (acc == null) acc = Double.Parse(accOperand) / Double.Parse(currOperand);
                                    else acc /= Double.Parse(currOperand);
                                    _currState = States.operatorEntered;
                                    currOperand = "";
                                    break;
                            }
                        }
                        if (_currState != States.error)
                        {
                            operation = character[0];
                            accOperand = acc.ToString();
                            if (character == "=")
                            {
                                _currState = States.equalitySignEntered;
                                break;
                            }
                            expression = accOperand + character;
                        }
                    }
                    else if (character.Equals("AC"))
                    {
                        acc = 0;
                        expression = "";
                        accOperand = "";
                        currOperand = "";
                        expression = "";
                    }
                    break;

                case States.equalitySignEntered:
                    if (character[0] >= '0' && character[0] <= '9')
                    {
                        expression = character;
                        currOperand = "";
                        accOperand = character;
                        acc = null;
                        _currState = States.waitingForFirstOperand;
                    }
                    else if (character[0] == ',' || character[0] == '.')
                    {
                        _currState = States.error;
                    }
                    else if (character.Equals("AC"))
                    {
                        acc = 0;
                        expression = "";
                        accOperand = "";
                        currOperand = "";
                        expression = "";
                    }
                    else if (character.Equals("-") || character.Equals("+") || character.Equals("/")
                            || character.Equals("*"))
                    {
                        switch (operation)
                        {
                            case '+':
                                if (acc == null) acc = Double.Parse(accOperand) + Double.Parse(currOperand);
                                else acc += Double.Parse(currOperand);
                                currOperand = "";
                                break;
                            case '-':
                                if (acc == null) acc = Double.Parse(accOperand) - Double.Parse(currOperand);
                                else acc -= Double.Parse(currOperand);
                                currOperand = "";
                                break;
                            case '*':
                                if (acc == null) acc = Double.Parse(accOperand) * Double.Parse(currOperand);
                                else acc *= Double.Parse(currOperand);
                                currOperand = "";
                                break;
                            case '/':
                                if (Double.Parse(currOperand) < 0.01)
                                {
                                    _currState = States.error;
                                    break;
                                }
                                if (acc == null) acc = Double.Parse(accOperand) / Double.Parse(currOperand);
                                else acc /= Double.Parse(currOperand);
                                currOperand = "";
                                break;

                        }
                        _currState = States.operatorEntered;
                        operation = character[0];
                        accOperand = acc.ToString();
                        expression = accOperand + character;
                        break;
                    }
                    break;
            }
        }
    }
}
