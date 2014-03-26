using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AwesomeControls.TextBox;

namespace AwesomeControls.TestProject
{
	public partial class TextBoxTest : Form
	{
		public TextBoxTest()
		{
			InitializeComponent();
			
			textBox1.Font = new Font(FontFamily.GenericMonospace, 10, FontStyle.Regular);
			textBox1.Text = string.Empty;
			/*
			txtXML.WordBreakingSequences.Add("<");
			txtXML.WordBreakingSequences.Add(">");
			*/

			textBox1.AutoSuggestMode = TextBox.TextBoxAutoSuggestMode.Popup;
			textBox1.AutoSuggestFilter = true;

			textBox1.AutoSuggestAcceptKeys.Add(Keys.D9 | Keys.Shift);           // Init();
			textBox1.AutoSuggestAcceptKeys.Add(Keys.Oemcomma | Keys.Shift);     // Buffer<T>
			textBox1.AutoSuggestAcceptKeys.Add(Keys.OemPeriod);     // Buffer.XXX

			textBox1.WordBreakingSequences.Add(".");
			textBox1.WordBreakingSequences.Add("(");
			textBox1.WordBreakingSequences.Add(")");

			InitSQL();
			InitWD();
			// InitHLSL();
			// InitVB();
			// InitCS();
			// InitConcertroid();
			// textBox1.Text = "SELECT * FROM Concertroid.Songs";
			
			textBox1.TextReplacements.Add("(", "()", -1);
			textBox1.TextReplacements.Add("{", "{}", -1);
			textBox1.TextReplacements.Add("[", "[]", -1);
			textBox1.TextReplacements.Add("\"", "\"\"", -1);
			textBox1.TextReplacements.Add("'", "''", -1);
			textBox1.TextReplacements.Add("<", "<>", -1);

			/*
			TextBoxFormatting tbf1 = new TextBoxFormatting(5, 10);
			// tbf1.Attributes |= TextBoxFormattingAttributes.Font;
			tbf1.Attributes |= TextBoxFormattingAttributes.Underline;
			tbf1.UnderlineStyle.Color = Color.Red;
			tbf1.UnderlineStyle.Type = TextBoxFormattingLineType.Wave;

			tbf1.Font = new Font(SystemFonts.MenuFont, FontStyle.Bold);
			textBox1.Formatting.Add(tbf1);
			*/

			textBox1.CaretSize = 1;
		}

		private void textBox1_KeyDown_VB(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				List<string> AutoCompleters = new List<string>();
				AutoCompleters.Add("Sub");
				AutoCompleters.Add("Function");
				AutoCompleters.Add("Class");
				AutoCompleters.Add("Enum");
				AutoCompleters.Add("Namespace");

				foreach (string str in AutoCompleters)
				{
					if (textBox1.Lines[textBox1.CurrentLineIndex].Trim().Contains(" " + str + " ") || textBox1.Lines[textBox1.CurrentLineIndex].Trim().StartsWith(str + " "))
					{
						textBox1.InsertText(textBox1.LineSeparatorString + "    ");
						textBox1.InsertText(textBox1.LineSeparatorString + "End " + str, false);
						e.Handled = true;
						e.SuppressKeyPress = true;
					}
				}
				
				if (textBox1.Lines[textBox1.Lines.Length - 1].StartsWith(" "))
				{
					string strtext = textBox1.Lines[textBox1.Lines.Length - 1];
					int i = 0;
					for (i = 0; i < strtext.Length; i++) if (strtext[i] != ' ') break;
					string tabs = strtext.Substring(0, i);
					textBox1.InsertText(textBox1.LineSeparatorString + tabs);
					e.Handled = true;
				}
			}
			else if (e.KeyCode == Keys.Back)
			{
				string strtext = textBox1.Lines[textBox1.Lines.Length - 1];
				if (strtext.EndsWith("    "))
				{
					textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 4);
					e.Handled = true;
				}
			}
		}

		private void InitConcertroid()
		{
			textBox1.AutoSuggestTerms.Add("Concertroid", "The Concertroid namespace contains all classes related to Concertroid.", Image.FromFile(@"Images/Namespace.png"));
			textBox1.AutoSuggestTerms.Add("Studio", "The Concertroid.Studio namespace contains all classes related to Concertroid Studio.", Image.FromFile(@"Images/Namespace.png"));
			textBox1.AutoSuggestTerms.Add("Renderer", "The Concertroid.Renderer namespace contains all classes related to Concertroid Renderer.", Image.FromFile(@"Images/Namespace.png"));
			
			textBox1.AutoSuggestTerms.Add("Concert", "Abstract class to represent the entire concert", Image.FromFile(@"Images/Class.png"));
			textBox1.AutoSuggestTerms.Add("Character", "The character to use in this performance", Image.FromFile(@"Images/Class.png"));
			textBox1.AutoSuggestTerms.Add("Costume", "The character to use in this performance", Image.FromFile(@"Images/Class.png"));
			textBox1.AutoSuggestTerms.Add("Performance", "A ", Image.FromFile(@"Images/Class.png"));
			textBox1.AutoSuggestTerms.Add("Song", "A song used in a Performance.", Image.FromFile(@"Images/Class.png"));
			textBox1.AutoSuggestTerms.Add("Producer", "The creator of a song.", Image.FromFile(@"Images/Class.png"));

			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Concert", Color.DarkCyan));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Character", Color.DarkCyan));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Costume", Color.DarkCyan));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Performance", Color.DarkCyan));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Song", Color.DarkCyan));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Producer", Color.DarkCyan));

		}

		private void InitHLSL()
		{
			#region Inherent types
			textBox1.AutoSuggestTerms.Add("bool", "Boolean value; true or false", Image.FromFile(@"Images/Structure.png"));
			textBox1.AutoSuggestTerms.Add("int", "32-bit signed integer", Image.FromFile(@"Images/Structure.png"));
			textBox1.AutoSuggestTerms.Add("uint", "32-bit unsigned integer", Image.FromFile(@"Images/Structure.png"));
			textBox1.AutoSuggestTerms.Add("dword", "32-bit unsigned integer", Image.FromFile(@"Images/Structure.png"));
			textBox1.AutoSuggestTerms.Add("half", "16-bit floating-point value", Image.FromFile(@"Images/Structure.png"));
			textBox1.AutoSuggestTerms.Add("float", "32-bit floating-point value", Image.FromFile(@"Images/Structure.png"));
			textBox1.AutoSuggestTerms.Add("double", "64-bit floating-point value", Image.FromFile(@"Images/Structure.png"));
			textBox1.AutoSuggestTerms.Add("texture", "Untyped texture for backwards compatibility", Image.FromFile(@"Images/Structure.png"));

			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("bool", Color.Red));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("int", Color.Red));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("uint", Color.Red));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("dword", Color.Red));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("half", Color.Red));

			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("float", Color.Red));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("float1", Color.Red));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("float2", Color.Red));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("float3", Color.Red));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("float4", Color.Red));

			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("double", Color.Red));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("texture", Color.Red));
			#endregion
			#region Classes
			textBox1.AutoSuggestTerms.Add("Buffer", "Buffer<Type> name;\r\n\r\nData is read from a buffer using an overloaded version of the Load HLSL intrinsic function that takes one input parameter (an integer index). A buffer is accessed like an array of elements.", Image.FromFile(@"Images/Class.png"));
			textBox1.AutoSuggestTerms.Add("Texture1D", Image.FromFile(@"Images/Class.png"));
			textBox1.AutoSuggestTerms.Add("Texture1DArray", Image.FromFile(@"Images/Class.png"));
			textBox1.AutoSuggestTerms.Add("Texture2D", Image.FromFile(@"Images/Class.png"));
			textBox1.AutoSuggestTerms.Add("Texture2DArray", Image.FromFile(@"Images/Class.png"));
			textBox1.AutoSuggestTerms.Add("Texture3D", Image.FromFile(@"Images/Class.png"));
			textBox1.AutoSuggestTerms.Add("TextureCube", Image.FromFile(@"Images/Class.png"));

			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Buffer", Color.DarkCyan));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Texture1D", Color.DarkCyan));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Texture1DArray", Color.DarkCyan));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Texture2D", Color.DarkCyan));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Texture2DArray", Color.DarkCyan));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Texture3D", Color.DarkCyan));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("TextureCube", Color.DarkCyan));
			#endregion

			#region Keywords

			textBox1.AutoSuggestTerms.Add("struct", "Declares an HLSL structure.\r\n\tstruct Name\r\n\t{\r\n\t\t[InterpolationModifier] Type[RxC] MemberName;\r\n\t\t...\r\n\t};", Image.FromFile(@"Images/Syntax.png"));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("struct", Color.Blue));
			textBox1.AutoSuggestTerms.Add("packoffset", "Optional shader constant packing keyword. Use this keyword to manually pack a shader constant when declaring a variable type. When packing a constant, you cannot mix constant types.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("packoffset", Color.Blue));
			textBox1.AutoSuggestTerms.Add("register", "Optional keyword for manually assigning a shader variable to a particular register.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("register", Color.Blue));
			#endregion
			
			#region Storage Classes
			textBox1.AutoSuggestTerms.Add("extern", "Mark a global variable as an external input to the shader; this is the default marking for all global variables. Cannot be combined with static.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("precise", "Prevent the compiler from making IEEE unsafe optimizations that affect this variable. The precise modifier also ensures that the compiler preserves the order of operations and that it must account for the possibility of NaN (not a number) and INF (infinite) values from constants and stream inputs. The ability to control optimizations in this way is useful when you write shaders for tessellation to maintain water-tight patch seams.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("shared", "Mark a variable for sharing between effects; this is a hint to the compiler.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("groupshared", "Mark a variable for thread-group-shared memory for compute shaders. In D3D10 the maximum total size of all variables with the groupshared storage class is 16kb, in D3D11 the maximum size is 32kb.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("static", "Mark a local variable so that it is initialized one time and persists between function calls. If the declaration does not include an initializer, the value is set to zero. A global variable marked static is not visible to an application.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("uniform", "Input only constant data. A uniform value comes from a constant register; each vertex shader or pixel shader invocation see the same initial value for a uniform variable.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("volatile", "Mark a variable that changes frequently; this is a hint to the compiler. This storage class modifier only applies to a local variable. The HLSL compiler currently ignores this storage class modifier.", Image.FromFile(@"Images/Syntax.png"));

			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("extern", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("precise", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("shared", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("groupshared", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("static", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("uniform", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("volatile", Color.Blue));
			#endregion
			#region Type Modifiers
			textBox1.AutoSuggestTerms.Add("const", "Mark a variable that cannot be changed by a shader, therefore, it must be initialized in the variable declaration. Global variables are considered const by default (suppress this behavior by supplying the /Gec flag to the compiler).", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("row_major", "Mark a variable that stores four components in a single row so they can be stored in a single constant register.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("column_major", "Mark a variable that stores 4 components in a single column to optimize matrix math.", Image.FromFile(@"Images/Syntax.png"));
			
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("const", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("row_major", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("column_major", Color.Blue));
			#endregion

			#region Interpolation Types
			textBox1.AutoSuggestTerms.Add("linear", "Interpolate between shader inputs; this is the default value if no interpolation modifier is specified.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("centroid", "Interpolate between samples that are somewhere within the covered area of the pixel (this may require extrapolating end points from a pixel center). Centroid sampling may improve antialiasing if a pixel is partially covered (even if the pixel center is not covered). The centroid modifier must be combined with either the linear or noperspective modifier.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("nointerpolation", "Do not interpolate the outputs of a vertex shader before passing them to a pixel shader.  When using an int/uint type, the only valid option is nointerpolation.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("noperspective", "Do not perform perspective-correction during interpolation. The noperspective modifier can be combined with the centroid modifier.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("sample", "Interpolate at sample location rather than at the pixel center. This causes the pixel shader to execute per-sample rather than per-pixel. Available in shader model 4.1 and later.", Image.FromFile(@"Images/Syntax.png"));
			
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("linear", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("centroid", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("nointerpolation", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("noperspective", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("sample", Color.Blue));
			#endregion

			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("snorm", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("unorm", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("vector", Color.Blue));

			#region Flow-control statements
			textBox1.AutoSuggestTerms.Add("break", "Exit the surrounding loop (do, for, while).", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("continue", "Stop executing the current loop (do, for, while), update the loop conditions, and begin executing from the top of the loop.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("discard", "Do not output the result of the current pixel.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("do", "Execute a series of statements continuously until the conditional expression fails.\r\n\r\n\t[fastopt] do\r\n\t{\r\n\t\t[{statement ...}]\r\n\t}\r\n\twhile ({condition});", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("fastopt", "Reduces the compile time but produces less aggressive optimizations. If you use this attribute, the compiler will not unroll loops.\r\n\r\nThis attribute affects only shader model targets that support break instructions. This attribute is available in shader model vs_2_x and shader model 3 and later. It is particularly useful in shader model 4 and later when the compiler compiles loops. The compiler simulates loops by default to evaluate whether it can unroll them. If you do not want the compiler to unroll loops, use this attribute to reduce compile time.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("for", "Iteratively executes a series of statements, based on the evaluation of the conditional expression.\r\n\r\n\t[unroll([{count}]) | loop | fastopt | allow_uav_condition] for ({initializer}; {conditional}; {iterator})\r\n\t{\r\n\t\t[{statement ...}]\r\n\t}", Image.FromFile(@"Images/Syntax.png"));

			textBox1.AutoSuggestTerms.Add("unroll", "Unroll the loop until it stops executing. Can optionally specify the maximum number of times the loop is to execute. Not compatible with the [loop] attribute.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("loop", "Generate code that uses flow control to execute each iteration of the loop. Not compatible with the [unroll] attribute.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("fastopt", "Reduces the compile time but produces less aggressive optimizations. If you use this attribute, the compiler will not unroll loops.\r\n\r\nThis attribute affects only shader model targets that support break instructions. This attribute is available in shader model vs_2_x and shader model 3 and later. It is particularly useful in shader model 4 and later when the compiler compiles loops. The compiler simulates loops by default to evaluate whether it can unroll them. If you do not want the compiler to unroll loops, use this attribute to reduce compile time.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("allow_uav_condition", "Allows a compute shader loop termination condition to be based off of a UAV read. The loop must not contain synchronization intrinsics.", Image.FromFile(@"Images/Syntax.png"));


			textBox1.AutoSuggestTerms.Add("if", "Conditionally execute a series of statements, based on the evaluation of the conditional expression.\r\n\r\n\t[branch | flatten] if ( {conditional} )\r\n\t{\r\n\t\t[{statement ...}]\r\n\t}", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("branch", "Evaluate only one side of the if statement depending on the given condition.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("flatten", "Evaluate both sides of the if statement and choose between the two resulting values.", Image.FromFile(@"Images/Syntax.png"));

			textBox1.AutoSuggestTerms.Add("switch", "Transfer control to a different statement block within the switch body depending on the value of a selector.\r\n\r\n\t[flatten | branch] [forcecase] [call] switch ({selector})\r\n\t{\r\n\t\tcase {value}:\r\n\t\t{\r\n\t\t\t[{statements ...}]\r\n\t\t\tbreak;\r\n\t\t}\r\n\t\t...\r\n\t}", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("forcecase", "Force a switch statement in the hardware.", Image.FromFile(@"Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("call", "The bodies of the individual cases in the switch will be moved into hardware subroutines and the switch will be a series of subroutine calls.", Image.FromFile(@"Images/Syntax.png"));

			textBox1.AutoSuggestTerms.Add("while", "Executes a statement block until the conditional expression fails.\r\n\r\n\t[unroll([{count}]) | loop | fastopt | allow_uav_condition] while ({conditional})\r\n\t{\r\n\t\t[{statement ...}]\r\n\t}", Image.FromFile(@"Images/Syntax.png"));

			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("break", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("continue", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("discard", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("do", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("fastopt", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("for", Color.Blue));
			
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("unroll", Color.DarkCyan));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("loop", Color.DarkCyan));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("fastopt", Color.DarkCyan));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("allow_uav_condition", Color.DarkCyan));

			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("if", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("branch", Color.DarkCyan));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("flatten", Color.DarkCyan));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("forcecase", Color.DarkCyan));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("call", Color.DarkCyan));

			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("switch", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("while", Color.Blue));
			#endregion

		}

		private void InitSQL()
		{

			#region Operators
			textBox1.AutoSuggestTerms.Add("AND");
			textBox1.AutoSuggestTerms.Add("OR");
			textBox1.AutoSuggestTerms.Add("NOT");
			textBox1.AutoSuggestTerms.Add("NULL");

			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("AND", Color.Gray));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("OR", Color.Gray));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("NOT", Color.Gray));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("NULL", Color.Gray));
			#endregion

			textBox1.AutoSuggestTerms.Add("ROW");
			textBox1.AutoSuggestTerms.Add("COLUMN");

			#region SELECT
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("SELECT", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("FROM", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("WHERE", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("ORDER", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("GROUP", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("BY", Color.Blue));

			textBox1.AutoSuggestTerms.Add("SELECT", "Selects data from a database.\r\n\r\n\tSELECT {columns} FROM {database} [WHERE {criteria ...}]", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("FROM", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("WHERE", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("ORDER", "Orders the result set by the specified column.", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("GROUP", "Groups the result set by the specified column.", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("BY", Image.FromFile("Images/Syntax.png"));
			#endregion
			#region INSERT
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("INSERT", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("ALL", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("INTO", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("VALUES", Color.Blue));

			textBox1.AutoSuggestTerms.Add("INSERT", "Inserts data into a table on the specified database.\r\n\r\n\tINSERT INTO {database} ({rows}) VALUES ({values})", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("ALL", "Inserts data into a table on the specified database.\r\n\r\n\tINSERT INTO {database} ({rows}) VALUES ({values})", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("INTO", "Inserts data into a table on the specified database.\r\n\r\n\tINSERT INTO {database} ({rows}) VALUES ({values})", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("VALUES", Image.FromFile("Images/Syntax.png"));
			#endregion

			#region CREATE
			TextBoxAutoSuggestTermItem termTABLE = new TextBoxAutoSuggestTermItem("TABLE", Image.FromFile("Images/Syntax.png"));
			TextBoxAutoSuggestTermItem termUSER = new TextBoxAutoSuggestTermItem("USER", Image.FromFile("Images/Syntax.png"));
			TextBoxAutoSuggestTermItem termDATABASE = new TextBoxAutoSuggestTermItem("DATABASE", Image.FromFile("Images/Syntax.png"));
			TextBoxAutoSuggestTermItem termTRIGGER = new TextBoxAutoSuggestTermItem("TRIGGER", Image.FromFile("Images/Syntax.png"));

			TextBoxAutoSuggestTermItem termCREATE = new TextBoxAutoSuggestTermItem("CREATE", "Creates a database, user, table, or other object on the server.\r\n\r\n\tCREATE DATABASE {database}\r\n\tCREATE USER {username} IDENTIFIED BY {password}\r\n\tCREATE TABLE {tablename} AS ( {column_name data_type null? }, ... )", Image.FromFile("Images/Syntax.png"));
			termCREATE.PreventDefaultSuggestions = true;
			termCREATE.AutoSuggestTerms.Add(termTABLE);
			termCREATE.AutoSuggestTerms.Add(termUSER);
			termCREATE.AutoSuggestTerms.Add(termDATABASE);
			termCREATE.AutoSuggestTerms.Add(termTRIGGER);
			textBox1.AutoSuggestTerms.Add(termCREATE);

			TextBoxAutoSuggestTermItem termALTER = new TextBoxAutoSuggestTermItem("ALTER", Image.FromFile("Images/Syntax.png"));
			termALTER.PreventDefaultSuggestions = true;
			termALTER.AutoSuggestTerms.Add(termTABLE);
			termALTER.AutoSuggestTerms.Add(termUSER);
			termALTER.AutoSuggestTerms.Add(termDATABASE);
			termALTER.AutoSuggestTerms.Add(termTRIGGER);
			textBox1.AutoSuggestTerms.Add(termALTER);
			
			TextBoxAutoSuggestTermItem termDROP = new TextBoxAutoSuggestTermItem("DROP", Image.FromFile("Images/Syntax.png"));
			termDROP.PreventDefaultSuggestions = true;
			termDROP.AutoSuggestTerms.Add(termTABLE);
			termDROP.AutoSuggestTerms.Add(termUSER);
			termDROP.AutoSuggestTerms.Add(termDATABASE);
			termDROP.AutoSuggestTerms.Add(termTRIGGER);
			textBox1.AutoSuggestTerms.Add(termDROP);

			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("CREATE", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("ALTER", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("DROP", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("TABLE", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("USER", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("TRIGGER", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("DATABASE", Color.Blue));
			#endregion

			#region Others
			textBox1.AutoSuggestTerms.Add("AS", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("IS", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("PRIMARY KEY", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add("AUTO_INCREMENT", "Indicates that the value in this column will be automatically incremented as each new row is inserted.", Image.FromFile("Images/Syntax.png"));

			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("AS", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("IS", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("PRIMARY", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("KEY", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("AUTO_INCREMENT", Color.Blue));
			#endregion

			#region Datatypes
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("BYTE", Color.Red));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("CHAR", Color.Red));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("VARCHAR", Color.Red));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("INT", Color.Red));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("MEMO", Color.Red));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("TEXT", Color.Red));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("DATE", Color.Red));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("DATETIME", Color.Red));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("TIMESTAMP", Color.Red));
			#endregion
			#region Blocks
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightBlock("\"", "\"", Color.DarkRed));
			#endregion

			/*
			#region Databases
			textBox1.AutoSuggestTerms.Add("dbo", Image.FromFile("Images/Namespace.png"));

			textBox1.AutoSuggestTerms.Add("AdventureWorks", "database AdventureWorks", Image.FromFile("Images/Database.png"));
			textBox1.AutoSuggestTerms.Add("BluePrintEmploymentOffice", "database", Image.FromFile("Images/Database.png"));
			textBox1.AutoSuggestTerms.Add("EmployeeLeaveRequest", Image.FromFile("Images/Database.png"));
			textBox1.AutoSuggestTerms.Add("FEMAReimbursement", Image.FromFile("Images/Database.png"));
			textBox1.AutoSuggestTerms.Add("IntegritSQL", Image.FromFile("Images/Database.png"));
			textBox1.AutoSuggestTerms.Add("ME2000", Image.FromFile("Images/Database.png"));
			textBox1.AutoSuggestTerms.Add("Meeting_Event_dev", Image.FromFile("Images/Database.png"));
			textBox1.AutoSuggestTerms.Add("PerformanceAppraisal", Image.FromFile("Images/Database.png"));
			textBox1.AutoSuggestTerms.Add("Workorders", Image.FromFile("Images/Database.png"));

			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("AdventureWorks", Color.FromArgb(0x2B, 0x91, 0xAF)));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("BluePrintEmploymentOffice", Color.FromArgb(0x2B, 0x91, 0xAF)));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("EmployeeLeaveRequest", Color.FromArgb(0x2B, 0x91, 0xAF)));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("FEMAReimbursement", Color.FromArgb(0x2B, 0x91, 0xAF)));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("IntegritSQL", Color.FromArgb(0x2B, 0x91, 0xAF)));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("ME2000", Color.FromArgb(0x2B, 0x91, 0xAF)));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Meeting_Event_dev", Color.FromArgb(0x2B, 0x91, 0xAF)));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("PerformanceAppraisal", Color.FromArgb(0x2B, 0x91, 0xAF)));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Workorders", Color.FromArgb(0x2B, 0x91, 0xAF)));
			#endregion
			*/

			textBox1.CaseSensitive = false;
		}

		// MENU:
		// WS_POPUP | WS_VISIBLE | WS_CLIPSIBLINGS | WS_CLIPCHILDREN
		// WS_EX_LEFT | WS_EX_LTRREADING | WS_EX_RIGHTSCROLLBAR | WS_EX_TOPMOST | WS_EX_TOOLWINDOW | WS_EX_COMPOSITED

		private void InitWD()
		{
			#region Databases
			textBox1.AutoSuggestTerms.Add("wd5_impl", Image.FromFile("Images/Namespace.png"));

			textBox1.AutoSuggestTerms.Add("cityoforlando1", Image.FromFile("Images/Database.png"));
			textBox1.AutoSuggestTerms.Add("cityoforlando2", Image.FromFile("Images/Database.png"));
			textBox1.AutoSuggestTerms.Add("cityoforlando3", Image.FromFile("Images/Database.png"));
			textBox1.AutoSuggestTerms.Add("cityoforlando4", Image.FromFile("Images/Database.png"));
			textBox1.AutoSuggestTerms.Add("cityoforlando5", Image.FromFile("Images/Database.png"));
			textBox1.AutoSuggestTerms.Add("cityoforlando6", Image.FromFile("Images/Database.png"));

			textBox1.AutoSuggestTerms.Add("Incentive_Certification", "The value associated with custom worktag 4.\r\n\r\nSource: Workday Delivered\r\nType: Single Instance\r\nCategory: Payroll", Image.FromFile("Images/Class.png"));
			textBox1.AutoSuggestTerms.Add("Worker", "The worker for this result line.\r\n\r\nSource: Workday Delivered\r\nType: Single Instance\r\nCategory: Payroll", Image.FromFile("Images/Class.png"));

			Color classColor = Color.FromArgb(0x2B, 0x91, 0xAF);
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Incentive_Certification", classColor));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Worker", classColor));
			#endregion
		}

		private void InitVB()
		{
			textBox1.KeyDown += new KeyEventHandler(textBox1_KeyDown_VB);
			
			// it only goes back as far as one word in the word chain, so it doesn't
			// know the difference between Async [CurrentWord] and Public Async
			// [CurrentWord]... we could fix this...

			#region Operators
			textBox1.AutoSuggestTerms.Add("And");
			textBox1.AutoSuggestTerms.Add("Or");
			textBox1.AutoSuggestTerms.Add("Not");
			textBox1.AutoSuggestTerms.Add("Null");

			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("And", Color.Gray));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Or", Color.Gray));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Not", Color.Gray));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Null", Color.Gray));
			#endregion

			#region Function/Sub
			TextBoxAutoSuggestTermItem termFunction = new TextBoxAutoSuggestTermItem("Function", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add(termFunction);
			TextBoxAutoSuggestTermItem termSub = new TextBoxAutoSuggestTermItem("Sub", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add(termSub);
			#endregion

			#region Access Modifiers/Async
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Private", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Public", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Friend", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Protected", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Async", Color.Blue));

			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Function", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Sub", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Inherits", Color.Blue));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Class", Color.Blue));

			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("As", Color.Blue));
			TextBoxAutoSuggestTermItem termAs = new TextBoxAutoSuggestTermItem("As", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add(termAs);

			TextBoxAutoSuggestTermItem termPrivate = new TextBoxAutoSuggestTermItem("Private", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add(termPrivate);

			TextBoxAutoSuggestTermItem termPublic = new TextBoxAutoSuggestTermItem("Public", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add(termPublic);

			TextBoxAutoSuggestTermItem termFriend = new TextBoxAutoSuggestTermItem("Friend", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add(termFriend);

			TextBoxAutoSuggestTermItem termProtected = new TextBoxAutoSuggestTermItem("Protected", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add(termProtected);

			TextBoxAutoSuggestTermItem termAsync = new TextBoxAutoSuggestTermItem("Async", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add(termAsync);

			TextBoxAutoSuggestTermItem termInherits = new TextBoxAutoSuggestTermItem("Inherits", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add(termInherits);

			TextBoxAutoSuggestTermItem termClass = new TextBoxAutoSuggestTermItem("Class", Image.FromFile("Images/Syntax.png"));
			textBox1.AutoSuggestTerms.Add(termClass);

			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("End", Color.Blue));
			TextBoxAutoSuggestTermItem termEnd = new TextBoxAutoSuggestTermItem("End", Image.FromFile("Images/Syntax.png"));
			termEnd.AutoSuggestTerms.Add(termClass);
			termEnd.AutoSuggestTerms.Add(termSub);
			termEnd.AutoSuggestTerms.Add(termFunction);
			textBox1.AutoSuggestTerms.Add(termEnd);

			termAsync.AutoSuggestTerms.Add(termFunction);
			termAsync.AutoSuggestTerms.Add(termPrivate);
			termAsync.AutoSuggestTerms.Add(termPublic);
			termAsync.AutoSuggestTerms.Add(termSub);

			#region PAsync - Async coming after an Access Modifier
			TextBoxAutoSuggestTermItem termPAsync = new TextBoxAutoSuggestTermItem("Async", Image.FromFile("Images/Syntax.png"));
			termPAsync.AutoSuggestTerms.Add(termFunction);
			termPAsync.AutoSuggestTerms.Add(termSub);

			termPrivate.AutoSuggestTerms.Add(termPAsync);
			termPublic.AutoSuggestTerms.Add(termPAsync);
			termFriend.AutoSuggestTerms.Add(termPAsync);
			termProtected.AutoSuggestTerms.Add(termPAsync);
			#endregion

			termPrivate.AutoSuggestTerms.Add(termClass);
			termPrivate.AutoSuggestTerms.Add(termSub);
			termPrivate.AutoSuggestTerms.Add(termFunction);

			termPublic.AutoSuggestTerms.Add(termClass);
			termPublic.AutoSuggestTerms.Add(termSub);
			termPublic.AutoSuggestTerms.Add(termFunction);

			termFriend.AutoSuggestTerms.Add(termClass);
			termFriend.AutoSuggestTerms.Add(termSub);
			termFriend.AutoSuggestTerms.Add(termFunction);

			termProtected.AutoSuggestTerms.Add(termClass);
			termProtected.AutoSuggestTerms.Add(termSub);
			termProtected.AutoSuggestTerms.Add(termFunction);
			termProtected.AutoSuggestTerms.Add(termFriend);
			#endregion

			#region Datatypes
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Short", Color.Red));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Integer", Color.Red));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("Long", Color.Red));
			textBox1.SyntaxHighlightObjects.Add(new TextBoxSyntaxHighlightTerm("String", Color.Red));

			termAs.AutoSuggestTerms.Add(new TextBoxAutoSuggestTermItem("Short", Image.FromFile("Images/Structure.png")));
			termAs.AutoSuggestTerms.Add(new TextBoxAutoSuggestTermItem("Integer", Image.FromFile("Images/Structure.png")));
			termAs.AutoSuggestTerms.Add(new TextBoxAutoSuggestTermItem("Long", Image.FromFile("Images/Structure.png")));
			termAs.AutoSuggestTerms.Add(new TextBoxAutoSuggestTermItem("String", Image.FromFile("Images/Class.png")));
			#endregion


			textBox1.CaseSensitive = false;
		}


		/*
		private void txtXML_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.OemPeriod && e.Shift)
			{
				if (txtXML.LastWord.StartsWith("<"))
				{
					int idx = txtXML.LastWord.LastIndexOf('<');
					if (idx == -1) return;
					if (txtXML.LastWord.EndsWith(">")) return;

					string tagName = txtXML.LastWord.Substring(idx + 1, txtXML.LastWord.Length - idx - 1);
					if (!txtXML.NextWord.StartsWith("</" + tagName + ">"))
					{
						txtXML.InsertText("</" + tagName + ">", false);
					}
				}
			}
		}
		*/
	}
}
