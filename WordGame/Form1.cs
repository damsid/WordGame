using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordGame
{
    public partial class Form1 : Form
    {
        // INSPIRATION FOR THIS PROJECT WAS TAKEN FROM NEW YORK TIMES' "WORDLE"
        // https://www.nytimes.com/games/wordle/index.html

        private Random rand = new Random();
        private List<TextBox> currentBoxes = new List<TextBox>();
        private List<string> WordList = new List<string>();
        private int CurrentLine = 1;
        private int Score = 0;
        private string CurrentWord = string.Empty;

        // This string will utilize a .txt file with over 5000 words to choose a random word
        private const string WORD_FILE_PATH = @"..\..\EnglishGameWords.txt";
        public Form1()
        {
            InitializeComponent();
            WordList = GetAllWords();
            StartNewGame();
        }
        private void StartNewGame()
        {
            // Takes a random word from the text file
            CurrentWord = WordList[rand.Next(WordList.Count)];
            CurrentLine = 1;
            btnSubmit.Enabled = true;
        }

        private List<string> GetAllWords()
        {
            // Using StreamReader, opens the txt file, the location of which is stored in a string
            List<string> allWords = new List<string>();
            using (StreamReader reader = new StreamReader(WORD_FILE_PATH))
            {
                while (!reader.EndOfStream)
                {
                    // Reads every line of the list of words
                    var nextLine = reader.ReadLine();
                    allWords.Add(nextLine);
                }
            }
            return allWords;
        }

   

        private void ColorBox(int index, TextBox t)
        {
            // This section of the code validates the letters and their positioning/index.
            // If there are no correct letters, display grey box
            if (!CurrentWord.Contains(t.Text))
            {
                t.BackColor = Color.Gray;
            }
            // If the letter is in the correct index, display green box
            else if (CurrentWord[index].ToString().ToLower() != t.Text.ToLower())
            {
                t.BackColor = Color.Yellow;
            }
            else
            {
                // If the letter is in the correct index, display green box
                t.BackColor = Color.LightGreen;
            }
        }

        private void EndGame()
        {
            // Display victory message
            MessageBox.Show("You won!");
            // Prevent users from entering further input
            btnSubmit.Enabled = false;
        }

        private string GetInput()
        {
            currentBoxes = new List<TextBox>();

            string tempString = String.Empty;
            // Switch statement figures out what line the user is working on
            switch (CurrentLine)
            {
                case 1: // The Add(textBox) part "draws" the boxes to show input
                    tempString = textBox1.Text
                            + textBox2.Text
                            + textBox3.Text
                            + textBox4.Text
                            + textBox5.Text;
                    currentBoxes.Add(textBox1);
                    currentBoxes.Add(textBox2);
                    currentBoxes.Add(textBox3);
                    currentBoxes.Add(textBox4);
                    currentBoxes.Add(textBox5);
                    break;
                case 2:
                    tempString = textBox6.Text
                            + textBox7.Text
                            + textBox8.Text
                            + textBox9.Text
                            + textBox10.Text;
                    currentBoxes.Add(textBox6);
                    currentBoxes.Add(textBox7);
                    currentBoxes.Add(textBox8);
                    currentBoxes.Add(textBox9);
                    currentBoxes.Add(textBox10);
                    break;
                case 3:
                    tempString = textBox11.Text
                            + textBox12.Text
                            + textBox13.Text
                            + textBox14.Text
                            + textBox15.Text;
                    currentBoxes.Add(textBox11);
                    currentBoxes.Add(textBox12);
                    currentBoxes.Add(textBox13);
                    currentBoxes.Add(textBox14);
                    currentBoxes.Add(textBox15);
                    break;
                case 4:
                    tempString = textBox16.Text
                            + textBox17.Text
                            + textBox18.Text
                            + textBox19.Text
                            + textBox20.Text;
                    currentBoxes.Add(textBox16);
                    currentBoxes.Add(textBox17);
                    currentBoxes.Add(textBox18);
                    currentBoxes.Add(textBox19);
                    currentBoxes.Add(textBox20);
                    break;
                case 5:
                    tempString = textBox21.Text
                            + textBox22.Text
                            + textBox23.Text
                            + textBox24.Text
                            + textBox25.Text;
                    currentBoxes.Add(textBox21);
                    currentBoxes.Add(textBox22);
                    currentBoxes.Add(textBox23);
                    currentBoxes.Add(textBox24);
                    currentBoxes.Add(textBox25);
                    break;
                case 6:
                    tempString = textBox26.Text
                            + textBox27.Text
                            + textBox28.Text
                            + textBox29.Text
                            + textBox30.Text;
                    currentBoxes.Add(textBox26);
                    currentBoxes.Add(textBox27);
                    currentBoxes.Add(textBox28);
                    currentBoxes.Add(textBox29);
                    currentBoxes.Add(textBox30);
                    break;
            }
            return tempString;
        }

        private bool ValidateInput(string input)
        {
            Regex rx = new Regex("^[a-zA-Z]+");

            if (input.Length == 5 && rx.IsMatch(input))
            {

                return true;

            }
            // If the input is in a wrong format, display the following message:
            MessageBox.Show("Please enter a valid, five-letter word.");
            return false;
        }

        private bool IsCorrectWord(string attempt)
        {
            // Checks if the input entered is correct - returns as a true if it is
            if (CurrentWord.Equals(attempt, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        private void EnglishGame_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WordGameHelp f2 = new WordGameHelp();
            f2.ShowDialog();
        }

        private void btnSubmit_Click_1(object sender, EventArgs e)
        {
            // First step is to get the user input
            var userWord = GetInput();

            // Validate data to prevent them from inputting the wrong format of data
            if (!ValidateInput(userWord))
            {
                // Proceeds to display an error message
                return;
            }

            // 
            bool isCorrect = IsCorrectWord(userWord);

            // This section of code colors the text boxes green, yellow, or grey           
            for (int i = 0; i < currentBoxes.Count(); i++)
            {
                ColorBox(i, currentBoxes[i]);
            }

            // If user gets the correct word, ends game
            if (isCorrect)
            {
                Score += 1;
                lblScore.Text = ("Score: " + Score);
                EndGame();
                return;
            }

            // If not, increment current line by 1
            CurrentLine++;
            if (CurrentLine > 6)
            {
                MessageBox.Show("You lost! " + "The correct word was: " + CurrentWord);
                btnSubmit.Enabled = false;
            }
        }

        private void btnCheat_Click(object sender, EventArgs e)
        {
            // This button allows the user to cheat and get the word to show up in a MessageBox
            // This does not end the game, instead it just reveals the answer
            MessageBox.Show("The word you are looking for is " + CurrentWord);
        }

        private void btnNewWord_Click(object sender, EventArgs e)
        {
            // This big segment of code completely wipes any data input and resets all the colors
            // There are more efficient ways of doing this, but due to time restrictions I had to go with this
            StartNewGame();
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox5.Text = String.Empty;
            textBox6.Text = String.Empty;
            textBox7.Text = String.Empty;
            textBox8.Text = String.Empty;
            textBox9.Text = String.Empty;
            textBox10.Text = String.Empty;
            textBox11.Text = String.Empty;
            textBox12.Text = String.Empty;
            textBox13.Text = String.Empty;
            textBox14.Text = String.Empty;
            textBox15.Text = String.Empty;
            textBox16.Text = String.Empty;
            textBox17.Text = String.Empty;
            textBox18.Text = String.Empty;
            textBox19.Text = String.Empty;
            textBox20.Text = String.Empty;
            textBox21.Text = String.Empty;
            textBox22.Text = String.Empty;
            textBox23.Text = String.Empty;
            textBox24.Text = String.Empty;
            textBox25.Text = String.Empty;
            textBox26.Text = String.Empty;
            textBox27.Text = String.Empty;
            textBox28.Text = String.Empty;
            textBox29.Text = String.Empty;
            textBox30.Text = String.Empty;
            textBox1.BackColor = Color.White;
            textBox2.BackColor = Color.White;
            textBox3.BackColor = Color.White;
            textBox4.BackColor = Color.White;
            textBox5.BackColor = Color.White;
            textBox6.BackColor = Color.White;
            textBox7.BackColor = Color.White;
            textBox8.BackColor = Color.White;
            textBox9.BackColor = Color.White;
            textBox10.BackColor = Color.White;
            textBox11.BackColor = Color.White;
            textBox12.BackColor = Color.White;
            textBox13.BackColor = Color.White;
            textBox14.BackColor = Color.White;
            textBox15.BackColor = Color.White;
            textBox16.BackColor = Color.White;
            textBox17.BackColor = Color.White;
            textBox18.BackColor = Color.White;
            textBox19.BackColor = Color.White;
            textBox20.BackColor = Color.White;
            textBox21.BackColor = Color.White;
            textBox22.BackColor = Color.White;
            textBox23.BackColor = Color.White;
            textBox24.BackColor = Color.White;
            textBox25.BackColor = Color.White;
            textBox26.BackColor = Color.White;
            textBox27.BackColor = Color.White;
            textBox28.BackColor = Color.White;
            textBox29.BackColor = Color.White;
            textBox30.BackColor = Color.White;
        }
    }
}
        

