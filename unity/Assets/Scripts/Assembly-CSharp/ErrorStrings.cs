using UnityEngine;

public class ErrorStrings : MonoBehaviour
{
	public static string ValueNull<VarType>(VarType nullVar, string nullVarName, string parentName = null)
	{
		if (IsEmptyString(parentName))
		{
			return string.Format("Error NUL - recieved null {0}-type variable named '{1}'", typeof(VarType).ToString(), nullVarName);
		}
		return string.Format("Error NUL - recieved null {0}-type variable named '{1}' under parent {2}", typeof(VarType).ToString(), nullVarName, parentName);
	}

	public static string ValueNull<VariableType>(VariableType maybeNullVariable1, string nameOfMaybeNullVariable1, VariableType maybeNullVariable2, string nameOfMaybeNullVariable2, string parentName = null)
	{
		string text = ((maybeNullVariable1 == null && maybeNullVariable2 == null) ? string.Format("Error NLS - recieved two null {0}-type variables named '{1}' and '{2}'", ShortenedTypeString<VariableType>(false), nameOfMaybeNullVariable1, nameOfMaybeNullVariable2) : ((maybeNullVariable1 == null) ? ValueNull(maybeNullVariable1, nameOfMaybeNullVariable1) : ((maybeNullVariable2 != null) ? string.Format("Error NNL - the function ErrorStrings.ValueNull was passed two NOT Null {0}-type variables named '{1}' and '{2}' - double check the error-handling functions that incorrectly said one of these values was null", ShortenedTypeString<VariableType>(false), nameOfMaybeNullVariable1, nameOfMaybeNullVariable2) : ValueNull(maybeNullVariable1, nameOfMaybeNullVariable1))));
		if (!IsEmptyString(parentName))
		{
			text = text + " under parent named '" + parentName + "'";
		}
		return text;
	}

	public static string UnableToFind<UnfoundVariableType>(UnfoundVariableType unfoundVariable, string nameUsedToSearch)
	{
		return string.Format("Error UFD - unable to find a {0} using search-name '{1}'", ShortenedTypeString<UnfoundVariableType>(false), nameUsedToSearch);
	}

	public static string UnableToFind<UnfoundVariableType>(string nameUsedToSearch)
	{
		return string.Format("Error UFD - unable to find a {0} using search-name '{1}'", ShortenedTypeString<UnfoundVariableType>(false), nameUsedToSearch);
	}

	public static string UnableToFind<UnfoundVariableType>(string nameUsedToSearch, int expectedTotalToFind)
	{
		return string.Format("Error UFD - unable to find {0} {1}/s using search-name '{2}'", expectedTotalToFind, ShortenedTypeString<UnfoundVariableType>(false), nameUsedToSearch);
	}

	public static string UnableToFind<UnfoundVariableType>(string nameUsedToSearch, int expectedTotalToFind, int totalActuallyFound)
	{
		return string.Format("Error UFD - found {0} instead of {1} {2}/s using search-name '{3}'", totalActuallyFound, expectedTotalToFind, ShortenedTypeString<UnfoundVariableType>(false), nameUsedToSearch);
	}

	public static string UnableToFind<UnfoundVariableType>(UnfoundVariableType unfoundVariable, string nameUsedToSearch, string parentSearchedUnder)
	{
		return string.Format("Error UFD - unable to find a {0} under '{1}' using search-name '{2}'", ShortenedTypeString<UnfoundVariableType>(false), parentSearchedUnder, nameUsedToSearch);
	}

	public static string UnableToFind<UnfoundVariableType>(string nameUsedToSearch, string parentSearchedUnder)
	{
		return string.Format("Error UFD - unable to find a {0} under '{1}' using search-name '{2}'", ShortenedTypeString<UnfoundVariableType>(false), parentSearchedUnder, nameUsedToSearch);
	}

	public static string UnableToFind<UnfoundVariableType>(string nameUsedToSearch, int expectedTotalToFind, string parentSearchedUnder)
	{
		return string.Format("Error UFD - unable to find {0} {1}/s under '{2}' using search-name '{3}'", expectedTotalToFind, ShortenedTypeString<UnfoundVariableType>(false), parentSearchedUnder, nameUsedToSearch);
	}

	public static string UnableToFind<UnfoundVariableType>(string nameUsedToSearch, int expectedTotalToFind, int totalActuallyFound, string parentSearchedUnder)
	{
		return string.Format("Error UFD - found {0} instead of {1} {2}/s under '{3}' using search-name '{4}'", totalActuallyFound, expectedTotalToFind, ShortenedTypeString<UnfoundVariableType>(false), parentSearchedUnder, nameUsedToSearch);
	}

	public static string UnableToFindInList<VarType>(VarType unlistedVar, string varName, string listName)
	{
		return string.Format("Error ULV - unable to find '{0}' of value '{1}' under list named '{2}'  {3}", varName, unlistedVar.ToString(), listName, TypeString<VarType>());
	}

	public static string ValueUnexpected<VarType>(VarType unexpectedVar, string varName)
	{
		return string.Format("Error EXV - recieved unexpected value for '{0}' of '{1}'  {2}", varName, unexpectedVar.ToString(), TypeString(unexpectedVar));
	}

	public static string CaseUnhandled<VarType>(VarType unhandledCase, string caseVariableName, string nameOfFunction)
	{
		return string.Format("Error UHA - a case statement in function named '{0}' does not yet contain code to handle the case of '{1}' for the variable named {2}  {3}", nameOfFunction, unhandledCase.ToString(), caseVariableName, TypeString(unhandledCase));
	}

	public static string UncodedButton(GameObject unhandledButton)
	{
		if (unhandledButton == null)
		{
			return NullButton();
		}
		return UncodedButton(unhandledButton.transform);
	}

	public static string UncodedButton(Component unhandledButton)
	{
		if (unhandledButton == null)
		{
			return NullButton();
		}
		return UncodedButton(unhandledButton.transform);
	}

	public static string UncodedButton(Transform unhandledButton)
	{
		if (unhandledButton == null)
		{
			return NullButton();
		}
		return string.Format("Error UHB - attempt to activate button named '{0}' when no code has been written to handle what that button does when activated", unhandledButton.name);
	}

	public static string NullButton()
	{
		return "Error UHB - attempt to activate a null button";
	}

	public static string ValueOutOfRange<VariableType>(VariableType outOfRangeValue, string outOfRangeVariableName, VariableType rangeMaximumValue)
	{
		return ValueOutOfRange(outOfRangeValue, outOfRangeVariableName, default(VariableType), rangeMaximumValue);
	}

	public static string ValueOutOfRange<VariableType>(VariableType outOfRangeValue, string outOfRangeVariableName, VariableType rangeMinimumValue, VariableType rangeMaximumValue)
	{
		return string.Format("Error OOR - recieved a '{0}' with a value of {1} out the expected range of {2} to {3}  {4}", outOfRangeVariableName, outOfRangeValue, rangeMinimumValue, rangeMaximumValue, TypeString<VariableType>());
	}

	public static string IndexOutOfRange(int outOfRangeIndexValue, int arrayLength)
	{
		return IndexOutOfRange(outOfRangeIndexValue, null, 0, arrayLength);
	}

	public static string IndexOutOfRange(int outOfRangeIndexValue, string indexVariableName, int arrayLength)
	{
		return IndexOutOfRange(outOfRangeIndexValue, indexVariableName, 0, arrayLength);
	}

	public static string IndexOutOfRange(int outOfRangeIndexValue, int minValidIndex, int arrayLength)
	{
		return IndexOutOfRange(outOfRangeIndexValue, null, minValidIndex, arrayLength);
	}

	public static string IndexOutOfRange<ArrayType>(int outOfRangeIndexValue, ArrayType[] array)
	{
		return IndexOutOfRange(outOfRangeIndexValue, null, 0, array.Length);
	}

	public static string IndexOutOfRange<ArrayType>(int outOfRangeIndexValue, string indexVariableName, ArrayType[] array)
	{
		return IndexOutOfRange(outOfRangeIndexValue, indexVariableName, 0, array.Length);
	}

	public static string IndexOutOfRange<ArrayType>(int outOfRangeIndexValue, int minValidIndex, ArrayType[] array)
	{
		return IndexOutOfRange(outOfRangeIndexValue, null, minValidIndex, array.Length);
	}

	public static string IndexOutOfRange(int outOfRangeIndexValue, string indexVariableName, int minValidIndex, int arrayLength)
	{
		if (IsEmptyString(indexVariableName))
		{
			return string.Format("Error OOR - recieved an index with an invalid value of {1}(outside range of {2} to {3})");
		}
		return string.Format("Error OOR - recieved an '{0}' index with an invalid value of {1}(outside range of {2} to {3})", indexVariableName, outOfRangeIndexValue, minValidIndex, arrayLength - 1);
	}

	public static string ActedAlreadyOn<VarType>(VarType alreadyActedVariable, string variableName, string actionDescription)
	{
		return string.Format("Error ACT - attempt to {0} already {0}ed '{1}' named '{2}'  {3}", actionDescription, alreadyActedVariable.ToString(), variableName, TypeString(alreadyActedVariable));
	}

	public static string ParseFailed<ParseType>(string unparsableString, string nameOfStringVariable)
	{
		return string.Format("Erorr UPA - unable to parse '{0}'s value of {1} into {2}", nameOfStringVariable, unparsableString, An(typeof(ParseType).ToString()));
	}

	public static string ParseFailed<ParseType>(string unparsableString, string nameOfStringVariable, string wholeUnparsableString)
	{
		return string.Format("Erorr UPA - unable to parse part of '{0}'s, specifically {1} into {2},({0}'s whole value was '{3}'", nameOfStringVariable, unparsableString, An(typeof(ParseType).ToString()), wholeUnparsableString);
	}

	public static string UnexpectedDuplicate<DuplicateType>(DuplicateType duplicateVariable, string nameOfDuplicateVariable, string nameOfListContainingDuplicate)
	{
		return UnexpectedDuplicate(duplicateVariable, nameOfDuplicateVariable, nameOfListContainingDuplicate, -1);
	}

	public static string UnexpectedDuplicate<DuplicateType>(DuplicateType duplicateVariable, string nameOfDuplicateVariable, string nameOfListContainingDuplicate, int indexOfDuplicateVariable)
	{
		if (indexOfDuplicateVariable == -1)
		{
			return string.Format("Error DUP - a duplicate '{0}' value of {1} was found under '{2}'  {3}", nameOfDuplicateVariable, duplicateVariable, nameOfListContainingDuplicate, TypeString<DuplicateType>());
		}
		return string.Format("Error DUP - a duplicate '{0}' value of {1} was found under '{2}' at index {3}  {4}", nameOfDuplicateVariable, duplicateVariable, nameOfListContainingDuplicate, indexOfDuplicateVariable, TypeString<DuplicateType>());
	}

	public static string AttemptToAddDuplicate<VariableType>(VariableType variableTriedToAdd, string nameOfVariable, string nameOfListTriedToAddTo)
	{
		return string.Format("Error ARL - attempt to add ANOTHER '{0}' with a value of {1} to the list '{2}', which already contained a {1}   {3}", nameOfVariable, variableTriedToAdd, nameOfListTriedToAddTo, TypeString(variableTriedToAdd));
	}

	public static string NoComponentFound<ComponentType>(GameObject componentlessGameObject)
	{
		if (componentlessGameObject == null)
		{
			return string.Format("Error UFC - unable to find a {0} Component under GameObject - because the GameObject was null!", typeof(ComponentType).ToString());
		}
		return string.Format("Error UFC - unable to find a {0} Component under GameObject named {1}", typeof(ComponentType).ToString(), componentlessGameObject.name);
	}

	public static string ValuesUnequal<VariableType>(VariableType firstVariable, string nameOfFirstVariable, VariableType secondVariable, string nameOfSecondVariable)
	{
		return string.Format("Error UEQ - the variables '{0}' and '{1}' were NOT equal as expected(their values were {2} and {3} respectively,  {4}", nameOfFirstVariable, nameOfSecondVariable, firstVariable, secondVariable, TypeString<VariableType>());
	}

	public static string ValueUneven(int unevenInteger, string nameOfInteger)
	{
		return string.Format("Error UEI - recieved uneven integer value of {0} for '{1}'", unevenInteger, nameOfInteger);
	}

	public static string ValueUneven(float unevenFloat, string nameOfFloat)
	{
		return string.Format("Error UEV - recieved uneven integer value of {0} for '{1}'", unevenFloat, nameOfFloat);
	}

	public static string FunctionAlreadyInitialized(string nameOfFunction)
	{
		return string.Format("Error AIN - attempt to initalize '{0}' more than once", nameOfFunction);
	}

	public static string FunctionAlreadyInitialized(string nameOfFunction, string nameOfClass)
	{
		return string.Format("Error AIN - attempt to initalize '{0}.{1}' more than once", nameOfClass, nameOfFunction);
	}

	public static string FunctionNotInitialized(string nameOfFunction)
	{
		return string.Format("Error NIN - attempt to call function '{0}' befores it was full initialized", nameOfFunction);
	}

	public static string FunctionNotInitialized(string nameOfFunction, string nameOfClass)
	{
		return string.Format("Error NIN - attempt to call function '{0}.{1}' befores it was full initialized", nameOfFunction, nameOfClass);
	}

	public static string FunctionNotOverriden(string nameOfFunction)
	{
		return string.Format("Error NOR - the function '{0}' was NOT overriden by a subclass as expected", nameOfFunction);
	}

	public static string FunctionNotOverriden(string nameOfFunction, string nameOfClass)
	{
		return string.Format("Error NOR - the function '{0}.{1}' was NOT overriden by a subclass as expected", nameOfClass, nameOfFunction);
	}

	public static string Impossible()
	{
		return "Error IMP - the impossible has happened!  REPENT!!";
	}

	private static string TypeString<VariableType>(VariableType variable, bool bracket = true)
	{
		return TypeString<VariableType>(bracket);
	}

	private static string TypeString<VariableType>(bool bracket = true)
	{
		string text = ShortenedTypeString<VariableType>();
		string text2 = "variable type: " + text;
		if (bracket)
		{
			return "(" + text2 + ")";
		}
		return text2;
	}

	private static string ShortenedTypeString<VariableType>(VariableType variable, bool bracket = true)
	{
		return ShortenedTypeString<VariableType>(bracket);
	}

	private static string ShortenedTypeString<VariableType>(bool bracket = true)
	{
		if (typeof(VariableType) == null)
		{
			if (bracket)
			{
				return "(null)";
			}
			return "null";
		}
		string text = typeof(VariableType).ToString();
		string[] array = text.Split('/');
		if (array.Length == 1)
		{
			return text;
		}
		return array[text.Length - 1];
	}

	private static bool IsEmptyString(string queryString)
	{
		return queryString == null || queryString.Equals(null) || queryString.Equals(string.Empty) || queryString.Length == 0;
	}

	private static bool IsntEmptyString(string queryString)
	{
		return !IsEmptyString(queryString);
	}

	private static string An(string text)
	{
		switch (text.Substring(0, 1))
		{
		case "a":
		case "e":
		case "i":
		case "o":
		case "u":
			return "an " + text;
		default:
			return "a " + text;
		}
	}
}
