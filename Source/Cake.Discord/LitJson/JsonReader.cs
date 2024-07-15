#pragma warning disable SA1124
#region Header
/*
 * JsonReader.cs
 *   Stream-like access to JSON text.
 *
 * The authors disclaim copyright to this source code. For more details, see
 * the COPYING file included with this distribution.
 */
#endregion

#pragma warning disable SA1003
#pragma warning disable SA1008
#pragma warning disable SA1009
#pragma warning disable SA1012
#pragma warning disable SA1013
#pragma warning disable SA1115
#pragma warning disable SA1116
#pragma warning disable SA1117
#pragma warning disable SA1119
#pragma warning disable SA1121
#pragma warning disable SA1122
#pragma warning disable SA1128
#pragma warning disable SA1129
#pragma warning disable SA1130
#pragma warning disable SA1200
#pragma warning disable SA1201
#pragma warning disable SA1202
#pragma warning disable SA1204
#pragma warning disable SA1210
#pragma warning disable SA1310
#pragma warning disable SA1311
#pragma warning disable SA1400
#pragma warning disable SA1401
#pragma warning disable SA1402
#pragma warning disable SA1403
#pragma warning disable SA1408
#pragma warning disable SA1413
#pragma warning disable SA1500
#pragma warning disable SA1503
#pragma warning disable SA1504
#pragma warning disable SA1507
#pragma warning disable SA1505
#pragma warning disable SA1508
#pragma warning disable SA1513
#pragma warning disable SA1515
#pragma warning disable SA1516
#pragma warning disable SA1519
#pragma warning disable SA1520
#pragma warning disable SA1600
#pragma warning disable SA1649
namespace Cake.Discord
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;


    namespace LitJson
    {
        internal enum JsonToken
        {
            None,

            ObjectStart,
            PropertyName,
            ObjectEnd,

            ArrayStart,
            ArrayEnd,

            Int,
            Long,
            Double,

            String,

            Boolean,
            Null
        }


        internal class JsonReader
        {
            #region Fields

            private static readonly IDictionary<int, IDictionary<int, int[]>> parse_table;

            private Stack<int> automaton_stack;
            private int current_input;
            private int current_symbol;
            private bool end_of_json;
            private bool end_of_input;
            private Lexer lexer;
            private bool parser_in_string;
            private bool parser_return;
            private bool read_started;
            private TextReader reader;
            private bool reader_is_owned;
            private bool skip_non_members;
            private object token_value;
            private JsonToken token;

            #endregion


            #region Public Properties

            public bool AllowComments
            {
                get { return lexer.AllowComments; }
                set { lexer.AllowComments = value; }
            }

            public bool AllowSingleQuotedStrings
            {
                get { return lexer.AllowSingleQuotedStrings; }
                set { lexer.AllowSingleQuotedStrings = value; }
            }

            public bool SkipNonMembers
            {
                get { return skip_non_members; }
                set { skip_non_members = value; }
            }

            public bool EndOfInput
            {
                get { return end_of_input; }
            }

            public bool EndOfJson
            {
                get { return end_of_json; }
            }

            public JsonToken Token
            {
                get { return token; }
            }

            public object Value
            {
                get { return token_value; }
            }

            #endregion


            #region Constructors

            static JsonReader()
            {
                parse_table = PopulateParseTable();
            }

            public JsonReader(string json_text) :
                this(new StringReader(json_text), true)
            {
            }

            public JsonReader(TextReader reader) :
                this(reader, false)
            {
            }

            private JsonReader(TextReader reader, bool owned)
            {
                if (reader == null)
                    throw new ArgumentNullException("reader");

                parser_in_string = false;
                parser_return = false;

                read_started = false;
                automaton_stack = new Stack<int>();
                automaton_stack.Push((int) ParserToken.End);
                automaton_stack.Push((int) ParserToken.Text);

                lexer = new Lexer(reader);

                end_of_input = false;
                end_of_json = false;

                skip_non_members = true;

                this.reader = reader;
                reader_is_owned = owned;
            }

            #endregion


            #region Static Methods

            private static IDictionary<int, IDictionary<int, int[]>> PopulateParseTable()
            {
                // See section A.2. of the manual for details
                IDictionary<int, IDictionary<int, int[]>> parse_table = new Dictionary<int, IDictionary<int, int[]>>();

                TableAddRow(parse_table, ParserToken.Array);
                TableAddCol(parse_table, ParserToken.Array, '[',
                    '[',
                    (int) ParserToken.ArrayPrime);

                TableAddRow(parse_table, ParserToken.ArrayPrime);
                TableAddCol(parse_table, ParserToken.ArrayPrime, '"',
                    (int) ParserToken.Value,

                    (int) ParserToken.ValueRest,
                    ']');
                TableAddCol(parse_table, ParserToken.ArrayPrime, '[',
                    (int) ParserToken.Value,
                    (int) ParserToken.ValueRest,
                    ']');
                TableAddCol(parse_table, ParserToken.ArrayPrime, ']',
                    ']');
                TableAddCol(parse_table, ParserToken.ArrayPrime, '{',
                    (int) ParserToken.Value,
                    (int) ParserToken.ValueRest,
                    ']');
                TableAddCol(parse_table, ParserToken.ArrayPrime, (int) ParserToken.Number,
                    (int) ParserToken.Value,
                    (int) ParserToken.ValueRest,
                    ']');
                TableAddCol(parse_table, ParserToken.ArrayPrime, (int) ParserToken.True,
                    (int) ParserToken.Value,
                    (int) ParserToken.ValueRest,
                    ']');
                TableAddCol(parse_table, ParserToken.ArrayPrime, (int) ParserToken.False,
                    (int) ParserToken.Value,
                    (int) ParserToken.ValueRest,
                    ']');
                TableAddCol(parse_table, ParserToken.ArrayPrime, (int) ParserToken.Null,
                    (int) ParserToken.Value,
                    (int) ParserToken.ValueRest,
                    ']');

                TableAddRow(parse_table, ParserToken.Object);
                TableAddCol(parse_table, ParserToken.Object, '{',
                    '{',
                    (int) ParserToken.ObjectPrime);

                TableAddRow(parse_table, ParserToken.ObjectPrime);
                TableAddCol(parse_table, ParserToken.ObjectPrime, '"',
                    (int) ParserToken.Pair,
                    (int) ParserToken.PairRest,
                    '}');
                TableAddCol(parse_table, ParserToken.ObjectPrime, '}',
                    '}');

                TableAddRow(parse_table, ParserToken.Pair);
                TableAddCol(parse_table, ParserToken.Pair, '"',
                    (int) ParserToken.String,
                    ':',
                    (int) ParserToken.Value);

                TableAddRow(parse_table, ParserToken.PairRest);
                TableAddCol(parse_table, ParserToken.PairRest, ',',
                    ',',
                    (int) ParserToken.Pair,
                    (int) ParserToken.PairRest);
                TableAddCol(parse_table, ParserToken.PairRest, '}',
                    (int) ParserToken.Epsilon);

                TableAddRow(parse_table, ParserToken.String);
                TableAddCol(parse_table, ParserToken.String, '"',
                    '"',
                    (int) ParserToken.CharSeq,
                    '"');

                TableAddRow(parse_table, ParserToken.Text);
                TableAddCol(parse_table, ParserToken.Text, '[',
                    (int) ParserToken.Array);
                TableAddCol(parse_table, ParserToken.Text, '{',
                    (int) ParserToken.Object);

                TableAddRow(parse_table, ParserToken.Value);
                TableAddCol(parse_table, ParserToken.Value, '"',
                    (int) ParserToken.String);
                TableAddCol(parse_table, ParserToken.Value, '[',
                    (int) ParserToken.Array);
                TableAddCol(parse_table, ParserToken.Value, '{',
                    (int) ParserToken.Object);
                TableAddCol(parse_table, ParserToken.Value, (int) ParserToken.Number,
                    (int) ParserToken.Number);
                TableAddCol(parse_table, ParserToken.Value, (int) ParserToken.True,
                    (int) ParserToken.True);
                TableAddCol(parse_table, ParserToken.Value, (int) ParserToken.False,
                    (int) ParserToken.False);
                TableAddCol(parse_table, ParserToken.Value, (int) ParserToken.Null,
                    (int) ParserToken.Null);

                TableAddRow(parse_table, ParserToken.ValueRest);
                TableAddCol(parse_table, ParserToken.ValueRest, ',',
                    ',',
                    (int) ParserToken.Value,
                    (int) ParserToken.ValueRest);
                TableAddCol(parse_table, ParserToken.ValueRest, ']',
                    (int) ParserToken.Epsilon);

                return parse_table;
            }

            private static void TableAddCol(IDictionary<int, IDictionary<int, int[]>> parse_table, ParserToken row,
                int col,
                params int[] symbols)
            {
                parse_table[(int) row].Add(col, symbols);
            }

            private static void TableAddRow(IDictionary<int, IDictionary<int, int[]>> parse_table, ParserToken rule)
            {
                parse_table.Add((int) rule, new Dictionary<int, int[]>());
            }

            #endregion


            #region Private Methods

            private void ProcessNumber(string number)
            {
                if (number.IndexOf('.') != -1 ||
                    number.IndexOf('e') != -1 ||
                    number.IndexOf('E') != -1)
                {

                    double n_double;
                    if (Double.TryParse(number, out n_double))
                    {
                        token = JsonToken.Double;
                        token_value = n_double;

                        return;
                    }
                }

                int n_int32;
                if (Int32.TryParse(number, out n_int32))
                {
                    token = JsonToken.Int;
                    token_value = n_int32;

                    return;
                }

                long n_int64;
                if (Int64.TryParse(number, out n_int64))
                {
                    token = JsonToken.Long;
                    token_value = n_int64;

                    return;
                }

                ulong n_uint64;
                if (UInt64.TryParse(number, out n_uint64))
                {
                    token = JsonToken.Long;
                    token_value = n_uint64;

                    return;
                }

                // Shouldn't happen, but just in case, return something
                token = JsonToken.Int;
                token_value = 0;
            }

            private void ProcessSymbol()
            {
                if (current_symbol == '[')
                {
                    token = JsonToken.ArrayStart;
                    parser_return = true;

                }
                else if (current_symbol == ']')
                {
                    token = JsonToken.ArrayEnd;
                    parser_return = true;

                }
                else if (current_symbol == '{')
                {
                    token = JsonToken.ObjectStart;
                    parser_return = true;

                }
                else if (current_symbol == '}')
                {
                    token = JsonToken.ObjectEnd;
                    parser_return = true;

                }
                else if (current_symbol == '"')
                {
                    if (parser_in_string)
                    {
                        parser_in_string = false;

                        parser_return = true;

                    }
                    else
                    {
                        if (token == JsonToken.None)
                            token = JsonToken.String;

                        parser_in_string = true;
                    }

                }
                else if (current_symbol == (int) ParserToken.CharSeq)
                {
                    token_value = lexer.StringValue;

                }
                else if (current_symbol == (int) ParserToken.False)
                {
                    token = JsonToken.Boolean;
                    token_value = false;
                    parser_return = true;

                }
                else if (current_symbol == (int) ParserToken.Null)
                {
                    token = JsonToken.Null;
                    parser_return = true;

                }
                else if (current_symbol == (int) ParserToken.Number)
                {
                    ProcessNumber(lexer.StringValue);

                    parser_return = true;

                }
                else if (current_symbol == (int) ParserToken.Pair)
                {
                    token = JsonToken.PropertyName;

                }
                else if (current_symbol == (int) ParserToken.True)
                {
                    token = JsonToken.Boolean;
                    token_value = true;
                    parser_return = true;

                }
            }

            private bool ReadToken()
            {
                if (end_of_input)
                    return false;

                lexer.NextToken();

                if (lexer.EndOfInput)
                {
                    Close();

                    return false;
                }

                current_input = lexer.Token;

                return true;
            }

            #endregion


            public void Close()
            {
                if (end_of_input)
                    return;

                end_of_input = true;
                end_of_json = true;

                if (reader_is_owned)
                    reader.Dispose();

                reader = null;
            }

            public bool Read()
            {
                if (end_of_input)
                    return false;

                if (end_of_json)
                {
                    end_of_json = false;
                    automaton_stack.Clear();
                    automaton_stack.Push((int) ParserToken.End);
                    automaton_stack.Push((int) ParserToken.Text);
                }

                parser_in_string = false;
                parser_return = false;

                token = JsonToken.None;
                token_value = null;

                if (!read_started)
                {
                    read_started = true;

                    if (!ReadToken())
                        return false;
                }


                int[] entry_symbols;

                while (true)
                {
                    if (parser_return)
                    {
                        if (automaton_stack.Peek() == (int) ParserToken.End)
                            end_of_json = true;

                        return true;
                    }

                    current_symbol = automaton_stack.Pop();

                    ProcessSymbol();

                    if (current_symbol == current_input)
                    {
                        if (!ReadToken())
                        {
                            if (automaton_stack.Peek() != (int) ParserToken.End)
                                throw new JsonException(
                                    "Input doesn't evaluate to proper JSON text");

                            if (parser_return)
                                return true;

                            return false;
                        }

                        continue;
                    }

                    try
                    {

                        entry_symbols =
                            parse_table[current_symbol][current_input];

                    }
                    catch (KeyNotFoundException e)
                    {
                        throw new JsonException((ParserToken) current_input, e);
                    }

                    if (entry_symbols[0] == (int) ParserToken.Epsilon)
                        continue;

                    for (int i = entry_symbols.Length - 1; i >= 0; i--)
                        automaton_stack.Push(entry_symbols[i]);
                }
            }

        }
    }
}
#pragma warning restore SA1003
#pragma warning restore SA1008
#pragma warning restore SA1009
#pragma warning restore SA1012
#pragma warning restore SA1013
#pragma warning restore SA1115
#pragma warning restore SA1116
#pragma warning restore SA1117
#pragma warning restore SA1119
#pragma warning restore SA1121
#pragma warning restore SA1122
#pragma warning restore SA1124
#pragma warning restore SA1128
#pragma warning restore SA1129
#pragma warning restore SA1130
#pragma warning restore SA1200
#pragma warning restore SA1201
#pragma warning restore SA1202
#pragma warning restore SA1204
#pragma warning restore SA1210
#pragma warning restore SA1310
#pragma warning restore SA1311
#pragma warning restore SA1400
#pragma warning restore SA1401
#pragma warning restore SA1402
#pragma warning restore SA1403
#pragma warning restore SA1408
#pragma warning restore SA1413
#pragma warning restore SA1500
#pragma warning restore SA1503
#pragma warning restore SA1504
#pragma warning restore SA1507
#pragma warning restore SA1505
#pragma warning restore SA1508
#pragma warning restore SA1513
#pragma warning restore SA1515
#pragma warning restore SA1516
#pragma warning restore SA1519
#pragma warning restore SA1520
#pragma warning restore SA1600
#pragma warning restore SA1649
