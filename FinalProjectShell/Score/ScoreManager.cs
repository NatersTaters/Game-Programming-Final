using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using System.Xml.Serialization;

namespace FinalProjectShell
{
	public class ScoreManager
	{
		private static string fileName = "scores";

		public List<Score> HighScores { get; private set; }
		public List<Score> Scores { get; private set; }

		public ScoreManager()
			: this(new List<Score>())
		{

		}

		public ScoreManager(List<Score> scores)
		{
			Scores = scores;

			UpdateHighScores();
		}

		/// <summary>
		/// Will add new score to the list of scores and sort them based on value
		/// </summary>
		/// <param name="score"></param>
		public void Add(Score score)
		{
			Scores.Add(score);

			Scores = Scores.OrderByDescending(c => c.Value).ToList();

			UpdateHighScores();
		}

		/// <summary>
		/// Will check if there is a file with highscores in the directory, if there is it will update the file with new highscore,
		/// if there is no file, it will create a new scores file with a new highscore
		/// </summary>
		/// <returns></returns>
		public static ScoreManager Load()
		{
			//if there isn't a file to load, create a new instance of "ScoreManager"
			if (!File.Exists(fileName))
			{
				return new ScoreManager();
			}

			//if file does exist
			using (var reader = new StreamReader(new FileStream(fileName, FileMode.Open)))
			{
				var serializer = new XmlSerializer(typeof(List<Score>));

				var scores = (List<Score>)serializer.Deserialize(reader);

				return new ScoreManager(scores);
			}
		}

		/// <summary>
		/// Will save current score to score file using XmlSerializer
		/// </summary>
		/// <param name="scoreManager"></param>
		public static void Save(ScoreManager scoreManager)
		{
			//overrides the file is it already exists
			using (var writer = new StreamWriter(new FileStream(fileName, FileMode.Create)))
			{
				var serializer = new XmlSerializer(typeof(List<Score>));

				serializer.Serialize(writer, scoreManager.Scores);
			}
		}

		/// <summary>
		/// Will update highscore list with the top 5 highest scores in the list
		/// </summary>
		public void UpdateHighScores()
		{
			HighScores = Scores.Take(5).ToList();
		}
	}
}
