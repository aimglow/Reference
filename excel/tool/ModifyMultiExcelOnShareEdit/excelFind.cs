//https://learn.microsoft.com/ja-jp/visualstudio/vsto/how-to-programmatically-search-for-text-in-worksheet-ranges


private void DemoFind() 
    {
        Excel.Range currentFind = null; 
        Excel.Range firstFind = null; 

        Excel.Range Fruits = Application.get_Range("A1", "B3");
        // You should specify all these parameters every time you call this method,
        // since they can be overridden in the user interface. 
        currentFind = Fruits.Find("apples", missing,
            Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart, 
            Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
            missing, missing); 

        while(currentFind != null) 
        { 
            // Keep track of the first range you find. 
            if (firstFind == null)
            {
                firstFind = currentFind; 
            }

            // If you didn't move to a new range, you are done.
            else if (currentFind.get_Address(Excel.XlReferenceStyle.xlA1)
                  == firstFind.get_Address(Excel.XlReferenceStyle.xlA1))
            {
                break;
            }

            currentFind.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
            currentFind.Font.Bold = true; 

            currentFind = Fruits.FindNext(currentFind); 
        }
			}
			

//https://learn.microsoft.com/en-us/dotnet/api/microsoft.office.interop.excel.range.replace?view=excel-pia
public bool Replace (object What, object Replacement, object LookAt, object SearchOrder, object MatchCase, object MatchByte, object SearchFormat, object ReplaceFormat);