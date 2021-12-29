using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace Proj5
{
    public partial class Form1 : Form
    {
        static Microsoft.FSharp.Collections.FSharpList<MyFSharpLibHw5.HW5P2.Article> alldata;
        static MySqlConnection conn;
        bool connected = false;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void F1Button_Click(object sender, EventArgs e)
        {
            string filename;
            filename = filenameBox.Text;
            alldata = MyFSharpLibHw5.HW5P2.readfile(filename);
            // Take the user's input, get an integer to pass to F# code
            string s_id = inputBox.Text;
            int id = MyFSharpLibHw5.HW5P2.stringToInteger(s_id);
            // Call the library function
            var title = MyFSharpLibHw5.HW5P2.getTitle(id, alldata);
            // Output the result
            outputBox.Text= String.Format("Title: {0}", title);
        }

        private void F2Button_Click(object sender, EventArgs e)
        {
            string filename;
            filename = filenameBox.Text;
            alldata = MyFSharpLibHw5.HW5P2.readfile(filename);
            // Take the user's input, get an integer to pass to F# code
            string s_id = inputBox.Text;
            int id = MyFSharpLibHw5.HW5P2.stringToInteger(s_id);
            // Call the library function
            var wordCount = MyFSharpLibHw5.HW5P2.wordCount(id, alldata);
            // Output the result
            outputBox.Text = String.Format("Number of Words in The Article: {0}", wordCount);
        }

        private void F3Button_Click(object sender, EventArgs e)
        {
            string filename;
            filename = filenameBox.Text;
            alldata = MyFSharpLibHw5.HW5P2.readfile(filename);
            // Take the user's input, get an integer to pass to F# code
            string s_id = inputBox.Text;
            int id = MyFSharpLibHw5.HW5P2.stringToInteger(s_id);
            // Call the library function
            var month = MyFSharpLibHw5.HW5P2.getMonthName(id, alldata);
            // Output the result
            outputBox.Text = String.Format("Month of Chosen Article: {0}", month);
        }

        private void F4Button_Click(object sender, EventArgs e)
        {
            string filename;
            filename = filenameBox.Text;
            alldata = MyFSharpLibHw5.HW5P2.readfile(filename);
            Microsoft.FSharp.Collections.FSharpList<string> publisherNames = MyFSharpLibHw5.HW5P2.publishers(alldata);
            string outputText = "Unique Publishers: "+Environment.NewLine;
            outputText += String.Join(Environment.NewLine, publisherNames);
            outputBox.Text = outputText;
        }

        private void F5Button_Click(object sender, EventArgs e)
        {
            string filename;
            filename = filenameBox.Text;
            alldata = MyFSharpLibHw5.HW5P2.readfile(filename);
            string outputText = "Unique Countries: "+Environment.NewLine;
            Microsoft.FSharp.Collections.FSharpList<string> countryNames = MyFSharpLibHw5.HW5P2.countries(alldata);
            outputText += String.Join(Environment.NewLine, countryNames);
            outputBox.Text = outputText;
        }

        private void F6Button_Click(object sender, EventArgs e)
        {
            string filename;
            filename = filenameBox.Text;
            alldata = MyFSharpLibHw5.HW5P2.readfile(filename);
            double overallguard = MyFSharpLibHw5.HW5P2.avgNewsguardscoreForArticles(alldata);
            outputBox.Text = String.Format("Average News Guard Score for All Articles: {0}", overallguard);
        }

        private void F7Button_Click(object sender, EventArgs e)
        {
            string filename;
            filename = filenameBox.Text;
            alldata = MyFSharpLibHw5.HW5P2.readfile(filename);
            Microsoft.FSharp.Collections.FSharpList<Tuple<string, int>> nArticles = MyFSharpLibHw5.HW5P2.numberOfArticlesEachMonth(alldata);
            string outputText;
            outputText = "Number of Articles for Each Month:"+Environment.NewLine;
            string output = MyFSharpLibHw5.HW5P2.buildHistogram(nArticles, alldata.Length, "");
            outputText += output;
            outputText = outputText.Replace("/n", Environment.NewLine);
            outputBox.Text = outputText;
        }

        private void F8Button_Click(object sender, EventArgs e)
        {
            string filename;
            filename = filenameBox.Text;
            alldata = MyFSharpLibHw5.HW5P2.readfile(filename);
            Microsoft.FSharp.Collections.FSharpList<Tuple<string, double>> reliablepct = MyFSharpLibHw5.HW5P2.reliableArticlePercentEachPublisher(alldata);
            string outputText;
            outputText = "Percentage of Articles That Are Reliable for Each Publisher: "+Environment.NewLine;
            Microsoft.FSharp.Collections.FSharpList<string> lines1 = MyFSharpLibHw5.HW5P2.printNamesAndPercentages(reliablepct);
            foreach (string line in lines1)
                outputText+=line+Environment.NewLine;
            outputBox.Text = outputText;
        }

        private void F9Button_Click(object sender, EventArgs e)
        {
            string filename;
            filename = filenameBox.Text;
            alldata = MyFSharpLibHw5.HW5P2.readfile(filename);
            // Call the library function to get the list of (string, double) for each country's average
            string outputText;
            outputText = "Average News Guard Score for Each Country: "+Environment.NewLine;
            // Call the library function transforming the list of pairs into a list of strings
            Microsoft.FSharp.Collections.FSharpList<Tuple<string, double>> list = MyFSharpLibHw5.HW5P2.avgNewsguardscoreEachCountry(alldata, MyFSharpLibHw5.HW5P2.countries(alldata));
            // Output the list of strings, one per line
            Microsoft.FSharp.Collections.FSharpList<string> lines1 = MyFSharpLibHw5.HW5P2.printNamesAndFloats(list);
            foreach (string line in lines1)
                outputText +=line+Environment.NewLine;
            outputBox.Text = outputText;
        }

        private void F10Button_Click(object sender, EventArgs e)
        {
            string filename;
            filename = filenameBox.Text;
            alldata = MyFSharpLibHw5.HW5P2.readfile(filename);
            // Call the library function to get the List of (string, double) pairs
            string outputText = "The Average News Guard Score for Each Political Bias Category: "+Environment.NewLine;
            var list1 = MyFSharpLibHw5.HW5P2.avgNewsguardscoreEachBias(alldata);
            // Call the library function to construct the histogram
            string histogram = MyFSharpLibHw5.HW5P2.buildHistogramFloat(list1, "");
            // output the string generated by the F# code
            outputText += histogram;
            outputText = outputText.Replace("/n", Environment.NewLine);
            outputBox.Text = outputText;
        }

        private void SQL1Button_Click(object sender, EventArgs e)
        {
            if (connected == false)
            {
                string connStr = "server=" + databaseBox1.Text + ";user=" + usernameBox.Text + ";database=" + databaseBox2.Text + ";port=" + portBox.Text + ";password=" + passwordBox.Text;  // change the database and password to test on your machine
                conn = new MySqlConnection(connStr);                                                     // must be these values when submitting to gradescope
                conn.Open();
                connected = true;
            }

            string stringInput = inputBox.Text;

            try
            {
                // Write (copy from queries folder) the query, using the news id read from the user
                //      You can use @" to begin a raw string, which allows for multiple lines in the string
                string query = String.Format(@" 
                                                SELECT title
                                                FROM news
                                                WHERE news_id = " + stringInput + @";
                                            ");
                // Build a Command which holds the query and the location of the target server
                //      Use the static MySqlConnection object conn which was initialized and opened at the beginning of the application
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);

                // Retrieve the results into a DataReader
                //      There are many methods for executing a command, use the one which returns a DataReader for the simplest solution.
                MySqlDataReader reader = cmd.ExecuteReader();

                // Output the header from the DataReader
                //     Use the .GetName function of the DataReader to get the column names for the header (just one column for function 1)
                string output = String.Format("{0}", reader.GetName(0));
                // Loop through the rows of the DataReader to output the values from the DataReader

                /*
                        Inside the loop
                        We use the DataReader object to get the values in the rows matching the columns of the header ouput before the loop.
                         -> reader.GetType(index) //Where 'Type' in GetType is to be replaced by the actual type of the attribute.
                         -> GetString for string type, GetInt32 for Integer types etc.
                        For Example: 
                        Console.WriteLine(String.Format("{0}\t{1}", reader.GetString(index1), reader.GetInt32(index2)));
                */
                while (reader.Read())
                {
                    output += Environment.NewLine;
                    output += String.Format("{0}", reader.GetString(0));
                }
                // Close the DataReader
                reader.Close();
                outputBox.Text = output;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SQL2Button_Click(object sender, EventArgs e)
        {
            if (connected == false)
            {
                string connStr = "server=" + databaseBox1.Text + ";user=" + usernameBox.Text + ";database=" + databaseBox2.Text + ";port=" + portBox.Text + ";password=" + passwordBox.Text;  // change the database and password to test on your machine
                conn = new MySqlConnection(connStr);                                                     // must be these values when submitting to gradescope
                conn.Open();
                connected = true;
            }
            MySqlDataReader reader;

            try
            {
                // Write (copy from queries folder) the query
                string query = String.Format(@" 
                                                SELECT news_id, LENGTH(body_text) AS length
                                                FROM news
                                                WHERE LENGTH(body_text)>100
                                                ORDER BY news_id;
                                            ");

                // Build a Command which holds the query and the location of the target server
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);

                // Retrieve the results into a DataReader
                reader = cmd.ExecuteReader();

                // Output the header from the DataReader
                string output = String.Format("{0}\t{1}", reader.GetName(0), reader.GetName(1));
                // Loop through the rows of the DataReader to output the values from the DataReader
                while (reader.Read())
                {
                    output += Environment.NewLine;
                    output +=String.Format("{0}\t{1}", reader.GetString(0), reader.GetInt32(1));
                }

                // Close the DataReader
                reader.Close();
                outputBox.Text = output;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SQL3Button_Click(object sender, EventArgs e)
        {
            if (connected == false)
            {
                string connStr = "server=" + databaseBox1.Text + ";user=" + usernameBox.Text + ";database=" + databaseBox2.Text + ";port=" + portBox.Text + ";password=" + passwordBox.Text;  // change the database and password to test on your machine
                conn = new MySqlConnection(connStr);                                                     // must be these values when submitting to gradescope
                conn.Open();
                connected = true;
            }

            try
            {
                // Write (copy from queries folder) the query
                string query = String.Format(@" 
                                                SELECT title, DATE_FORMAT(STR_TO_DATE(publish_date, '%c/%d/%y'), '%M') AS Month
                                                FROM news
                                                ORDER BY STR_TO_DATE(publish_date, '%m/%d/%y')
                                            ");

                // Build a Command which holds the query and the location of the target server
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);

                // Retrieve the results into a DataReader
                MySqlDataReader reader = cmd.ExecuteReader();

                // Output the header from the DataReader
                string output = String.Format("{0}\t{1}", reader.GetName(0), reader.GetName(1));

                // Loop through the rows of the DataReader to output the values from the DataReader
                while (reader.Read())
                {
                    output += Environment.NewLine;
                    output += String.Format("{0}\t{1}", reader.GetString(0), reader.GetString(1));
                }

                // Close the DataReader
                reader.Close();
                outputBox.Text = output;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SQL4Button_Click(object sender, EventArgs e)
        {
            if (connected == false)
            {
                string connStr = "server=" + databaseBox1.Text + ";user=" + usernameBox.Text + ";database=" + databaseBox2.Text + ";port=" + portBox.Text + ";password=" + passwordBox.Text;  // change the database and password to test on your machine
                conn = new MySqlConnection(connStr);                                                     // must be these values when submitting to gradescope
                conn.Open();
                connected = true;
            }

            try
            {
                // Write (copy from queries folder) the query
                string query = String.Format(@" 
                                                SELECT publisher
                                                FROM publisher_table
                                                JOIN news
                                                USING (publisher_id)
                                                GROUP BY publisher
                                                ORDER BY publisher;
                                            ");

                // Build a Command which holds the query and the location of the target server
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);

                // Retrieve the results into a DataReader
                MySqlDataReader reader = cmd.ExecuteReader();

                // Output the header from the DataReader
                string output = String.Format("{0}", reader.GetName(0));

                // Loop through the rows of the DataReader to output the values from the DataReader
                while (reader.Read())
                {
                    output += Environment.NewLine;
                    output += String.Format("{0}", reader.GetString(0));
                }

                // Close the DataReader
                reader.Close();
                outputBox.Text = output;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SQL5Button_Click(object sender, EventArgs e)
        {
            if (connected == false)
            {
                string connStr = "server=" + databaseBox1.Text + ";user=" + usernameBox.Text + ";database=" + databaseBox2.Text + ";port=" + portBox.Text + ";password=" + passwordBox.Text;  // change the database and password to test on your machine
                conn = new MySqlConnection(connStr);                                                     // must be these values when submitting to gradescope
                conn.Open();
                connected = true;
            }

            try
            {
                // Write (copy from queries folder) the query
                string query = String.Format(@" 
                                                SELECT country, COUNT(news_id) AS articleCount
                                                FROM country_table
                                                LEFT JOIN news
                                                USING (country_id)
                                                GROUP BY country
                                                ORDER BY articleCount DESC;
                                            ");

                // Build a Command which holds the query and the location of the target server
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);

                // Retrieve the results into a DataReader
                MySqlDataReader reader = cmd.ExecuteReader();

                // Output the header from the DataReader
                string output = String.Format("{0}\t{1}", reader.GetName(0), reader.GetName(1));

                // Loop through the rows of the DataReader to output the values from the DataReader
                while (reader.Read())
                {
                    output += Environment.NewLine;
                    output += String.Format("{0}\t{1}", reader.GetString(0), reader.GetInt32(1));
                }

                // Close the DataReader
                reader.Close();
                outputBox.Text = output;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SQL6Button_Click(object sender, EventArgs e)
        {
            if (connected == false)
            {
                string connStr = "server=" + databaseBox1.Text + ";user=" + usernameBox.Text + ";database=" + databaseBox2.Text + ";port=" + portBox.Text + ";password=" + passwordBox.Text;  // change the database and password to test on your machine
                conn = new MySqlConnection(connStr);                                                     // must be these values when submitting to gradescope
                conn.Open();
                connected = true;
            }

            try
            {
                // Write (copy from queries folder) the query
                string query = String.Format(@" 
                                                SELECT ROUND(AVG(news_guard_score),3) AS `Average Score`
                                                FROM news;
                                            ");

                // Build a Command which holds the query and the location of the target server
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);

                // Retrieve the results into a DataReader
                MySqlDataReader reader = cmd.ExecuteReader();

                // Output the header from the DataReader
                string output = String.Format("{0}", reader.GetName(0));

                // Loop through the rows of the DataReader to output the values from the DataReader
                while (reader.Read())
                {
                    output += Environment.NewLine;
                    output += String.Format("{0:N3}", reader.GetFloat(0));
                }

                // Close the DataReader
                reader.Close();
                outputBox.Text = output;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SQL7Button_Click(object sender, EventArgs e)
        {
            if (connected == false)
            {
                string connStr = "server=" + databaseBox1.Text + ";user=" + usernameBox.Text + ";database=" + databaseBox2.Text + ";port=" + portBox.Text + ";password=" + passwordBox.Text;  // change the database and password to test on your machine
                conn = new MySqlConnection(connStr);                                                     // must be these values when submitting to gradescope
                conn.Open();
                connected = true;
            }

            try
            {
                // Write (copy from queries folder) the query
                string query = String.Format(@" 
                                                SELECT month, numArticles, overall, ROUND(100*numArticles/overall,3) AS percentage
                                                FROM
                                                (
                                                SELECT month, monthnum, COUNT(publish_date) AS numArticles, overallCount AS overall
                                                FROM
                                                (
                                                SELECT DATE_FORMAT(STR_TO_DATE(publish_date, '%m/%d/%y'), '%M') AS month, 
                                                       DATE_FORMAT(STR_TO_DATE(publish_date, '%m/%d/%y'), '%m') AS monthnum,
	                                                   publish_date
                                                FROM news
                                                ) AS T1
                                                JOIN
                                                (
                                                SELECT COUNT(*) overallCount FROM news
                                                ) AS T2
                                                GROUP BY month, monthnum, overallCount
                                                ) AS T3
                                                ORDER BY monthnum;
                                            ");

                // Build a Command which holds the query and the location of the target server
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);

                // Retrieve the results into a DataReader
                MySqlDataReader reader = cmd.ExecuteReader();

                // Output the header from the DataReader
                string output = String.Format("{0}\t{1}\t{2}\t{3}", reader.GetName(0), reader.GetName(1), reader.GetName(2), reader.GetName(3));

                // Loop through the rows of the DataReader to output the values from the DataReader
                while (reader.Read())
                {
                    output += Environment.NewLine;
                    output += String.Format("{0}\t{1}\t{2}\t{3:N3}", reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetFloat(3));
                }

                // Close the DataReader
                reader.Close();
                outputBox.Text = output;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SQL8Button_Click(object sender, EventArgs e)
        {
            if (connected == false)
            {
                string connStr = "server=" + databaseBox1.Text + ";user=" + usernameBox.Text + ";database=" + databaseBox2.Text + ";port=" + portBox.Text + ";password=" + passwordBox.Text;  // change the database and password to test on your machine
                conn = new MySqlConnection(connStr);                                                     // must be these values when submitting to gradescope
                conn.Open();
                connected = true;
            }

            try
            {
                // Write (copy from queries folder) the query
                string query = String.Format(@" 
                                                SELECT publisher, ROUND(AVG(reliability)*100, 3) AS percentage
                                                FROM news
                                                JOIN publisher_table
                                                USING (publisher_id)
                                                GROUP BY publisher
                                                ORDER BY percentage DESC, publisher;
                                            ");

                // Build a Command which holds the query and the location of the target server
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);

                // Retrieve the results into a DataReader
                MySqlDataReader reader = cmd.ExecuteReader();

                // Output the header from the DataReader
                string output = String.Format("{0}\t{1}", reader.GetName(0), reader.GetName(1));

                // Loop through the rows of the DataReader to output the values from the DataReader
                while (reader.Read())
                {
                    output += Environment.NewLine;
                    output += String.Format("{0}\t{1:N3}", reader.GetString(0), reader.GetFloat(1));
                }

                // Close the DataReader
                reader.Close();
                outputBox.Text = output;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SQL9Button_Click(object sender, EventArgs e)
        {
            if (connected == false)
            {
                string connStr = "server=" + databaseBox1.Text + ";user=" + usernameBox.Text + ";database=" + databaseBox2.Text + ";port=" + portBox.Text + ";password=" + passwordBox.Text;  // change the database and password to test on your machine
                conn = new MySqlConnection(connStr);                                                     // must be these values when submitting to gradescope
                conn.Open();
                connected = true;
            }

            try
            {
                // Write (copy from queries folder) the query
                string query = String.Format(@" 
                                                SELECT country, ROUND(AVG(news_guard_score),3) AS avg_news_score
                                                FROM news
                                                JOIN country_table
                                                USING (country_id)
                                                GROUP BY country
                                                ORDER BY AVG(news_guard_score) DESC, country ASC;
                                            ");

                // Build a Command which holds the query and the location of the target server
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);

                // Retrieve the results into a DataReader
                MySqlDataReader reader = cmd.ExecuteReader();

                // Output the header from the DataReader
                string output = String.Format("{0}\t{1}", reader.GetName(0), reader.GetName(1));

                // Loop through the rows of the DataReader to output the values from the DataReader
                while (reader.Read())
                {
                    output += Environment.NewLine;
                    output += String.Format("{0}\t{1:N3}", reader.GetString(0), reader.GetFloat(1));
                }

                // Close the DataReader
                reader.Close();
                outputBox.Text = output;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SQL10Button_Click(object sender, EventArgs e)
        {
            if (connected == false)
            {
                string connStr = "server=" + databaseBox1.Text + ";user=" + usernameBox.Text + ";database=" + databaseBox2.Text + ";port=" + portBox.Text + ";password=" + passwordBox.Text;  // change the database and password to test on your machine
                conn = new MySqlConnection(connStr);                                                     // must be these values when submitting to gradescope
                conn.Open();
                connected = true;
            }

            try
            {
                // Write (copy from queries folder) the query
                string query = String.Format(@" 
                                                SELECT author, political_bias, COUNT(*) AS numArticles
                                                FROM news
                                                JOIN news_authors
                                                USING (news_id)
                                                JOIN author_table
                                                USING (author_id)
                                                JOIN political_bias_table
                                                USING (political_bias_id)
                                                GROUP BY author, political_bias
                                                ORDER BY author, COUNT(*) DESC, political_bias;
                                            ");

                // Build a Command which holds the query and the location of the target server
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);

                // Retrieve the results into a DataReader
                MySqlDataReader reader = cmd.ExecuteReader();

                // Output the header from the DataReader
                string output = String.Format("{0}\t{1}\t{2}", reader.GetName(0), reader.GetName(1), reader.GetName(2));

                // Loop through the rows of the DataReader to output the values from the DataReader
                while (reader.Read())
                {
                    output += Environment.NewLine;
                    output += String.Format("{0}\t{1}\t{2}", reader.GetString(0), reader.GetString(1), reader.GetInt32(2));
                }

                // Close the DataReader
                reader.Close();
                outputBox.Text = output;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
