//Angel Arroyo
//Edd Castillo
//Victor Hurtado

using Google.Cloud.Vision.V1;
using System;

namespace VisionApiDemo
{
    class Program
    {   
        static void Main(string[] args)
        {
            var client = ImageAnnotatorClient.Create();
            string[] pictures = {"obama_sad.jpg", "face_surprise.jpg", "face_no_surprise.png"};
            //string[] pictures = {"eiffel_text.jpg"};
            int surpriseCount = 0;
            int happyCount = 0;
            int angryCount = 0;
            int sadCount = 0;
            //int finalEmotion = 0;

            foreach (var picture in pictures)
            {
            var image = Image.FromUri("gs://bean_buckets/" + picture);
            var response = client.DetectLandmarks(image);
            var response2 = client.DetectText(image);
            var response3 = client.DetectFaces(image);
            int count = 1;

            Console.WriteLine($"Picture: {picture}");

            foreach (var annotation in response2)
            {
                if (annotation.Description != null)
                {
                    Console.WriteLine("Text detection: " + annotation.Description);
                }
            }

            foreach (var annotation in response)
            {
                if (annotation.Description != null)
                {
                    Console.WriteLine("Landmark detection: " + annotation.Description);
                }
            }             

                foreach (var annotation in response3){
                Console.WriteLine("Face {0}:", count++);
                    Console.WriteLine($"Surprise: {annotation.SurpriseLikelihood}");
                    Console.WriteLine($"Happy: {annotation.JoyLikelihood}");
                    Console.WriteLine($"Angry: {annotation.AngerLikelihood}");
                    Console.WriteLine($"Sad: {annotation.SorrowLikelihood}");
                    Console.WriteLine("");
                    
        switch ($"{annotation.SurpriseLikelihood}")
      {
          case "VeryUnlikely":
              surpriseCount += 1;
              break;
          case "Possible":
              surpriseCount += 2;
              break;
          case "Likely":
              surpriseCount += 3;   
              break; 
          default:
              surpriseCount +=0;
              break;
      }

      switch ($"{annotation.JoyLikelihood}")
      {
          case "VeryUnlikely":
              happyCount += 1;
              break;
          case "Possible":
              happyCount += 2;
              break;
          case "Likely":
              happyCount += 3;
              break;    
          default:
              happyCount +=0;
              break;
      }

      switch ($"{annotation.AngerLikelihood}")
      {
          case "VeryUnlikely":
              angryCount += 1;
              break;
          case "Possible":
              angryCount += 2;
              break;
          case "Likely":
              angryCount += 3; 
              break;   
          default:
              angryCount +=0;
              break;
      }

      switch ($"{annotation.SorrowLikelihood}")
      {
          case "VeryUnlikely":
              sadCount += 1;
              break;
          case "Possible":
              sadCount += 2;
              break;
          case "Likely":
              sadCount += 3;  
              break;  
          default:
              sadCount +=0;
              break;
      }
                }
                Console.WriteLine("Surprise Count:" + surpriseCount);
                Console.WriteLine("Sad Count:" + sadCount);
                Console.WriteLine("Happy Count:" + happyCount);
                Console.WriteLine("Angery Count:" + angryCount);
                
            }
            if(surpriseCount> sadCount && surpriseCount> happyCount && surpriseCount> angryCount)
                Console.WriteLine("Most Surprised");
                else if(sadCount> surpriseCount && sadCount> happyCount && sadCount> angryCount)
                Console.WriteLine("Most Sad");
                else if(happyCount> surpriseCount && happyCount> sadCount && happyCount> angryCount)
                Console.WriteLine("Most Happy");
                else if(angryCount> surpriseCount && angryCount> sadCount && angryCount> happyCount)
                Console.WriteLine("Most Angry");
        }
    }
}