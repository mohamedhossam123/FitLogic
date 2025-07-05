
using Microsoft.EntityFrameworkCore;
using BackEnd.Models; 

namespace MyApiProject.Services
{

    public class WorkoutService : IWorkoutService
    {
        private readonly Dictionary<string, List<ExerciseDTO>> _defaultWorkoutExercises = new Dictionary<string, List<ExerciseDTO>>();
        private readonly FitLogicContext _context;

        public WorkoutService(FitLogicContext context) 
        {
            _context = context;
            InitializeDefaultWorkoutExercises();
        }

        private void InitializeDefaultWorkoutExercises()
        {
            _defaultWorkoutExercises.Clear();
            _defaultWorkoutExercises.Add("Beginner Lose Fat Day 1", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Bodyweight Squats", Sets = "3", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "Push-ups", Sets = "3", Reps = "10-15", Notes = "Knees if needed" },
                new ExerciseDTO { Name = "Jumping Jacks", Sets = "3", Reps = "30 sec", Notes = "" },
                new ExerciseDTO { Name = "Plank", Sets = "3", Reps = "30 sec", Notes = "Core stability" },
                new ExerciseDTO { Name = "Bodyweight Lunges", Sets = "3", Reps = "12-15 per leg", Notes = "" },
                new ExerciseDTO { Name = "Bodyweight Rows", Sets = "3", Reps = "10-12", Notes = "Use a low bar or TRX" }
            });
            _defaultWorkoutExercises.Add("Beginner Lose Fat Day 2", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Walking Lunges", Sets = "3", Reps = "12-15 per leg", Notes = "" },
                new ExerciseDTO { Name = "Incline Push-ups", Sets = "3", Reps = "10-15", Notes = "" },
                new ExerciseDTO { Name = "Mountain Climbers", Sets = "3", Reps = "30 sec", Notes = "" },
                new ExerciseDTO { Name = "Glute Bridge", Sets = "3", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "Bodyweight Calf Raises", Sets = "3", Reps = "15-20", Notes = "" }
            });
            _defaultWorkoutExercises.Add("Beginner Lose Fat Day 3", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Step-ups", Sets = "3", Reps = "12-15 per leg", Notes = "" },
                new ExerciseDTO { Name = "Shoulder Taps", Sets = "3", Reps = "20", Notes = "Core and shoulders" },
                new ExerciseDTO { Name = "Standing Calf Raises", Sets = "3", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "Superman", Sets = "3", Reps = "15", Notes = "Lower back" },
                new ExerciseDTO { Name = "Bodyweight Glute Bridge", Sets = "3", Reps = "15-20", Notes = "" }
            });
            _defaultWorkoutExercises.Add("Beginner Lose Fat Day 4", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Reverse Lunges", Sets = "3", Reps = "12-15 per leg", Notes = "" },
                new ExerciseDTO { Name = "Bench Dips", Sets = "3", Reps = "10-12", Notes = "Triceps" },
                new ExerciseDTO { Name = "Standing Dumbbell Curl", Sets = "3", Reps = "12-15", Notes = "" },
                new ExerciseDTO { Name = "Side Plank", Sets = "3", Reps = "20 sec per side", Notes = "" },
                new ExerciseDTO { Name = "Bodyweight Step-ups", Sets = "3", Reps = "12-15 per leg", Notes = "" }
            });
            _defaultWorkoutExercises.Add("Beginner Lose Fat Day 5", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Goblet Squat", Sets = "3", Reps = "12-15", Notes = "" },
                new ExerciseDTO { Name = "Standing Shoulder Press", Sets = "3", Reps = "10-12", Notes = "" },
                new ExerciseDTO { Name = "Mountain Climbers", Sets = "3", Reps = "20", Notes = "" },
                new ExerciseDTO { Name = "Bird Dog", Sets = "3", Reps = "10 per side", Notes = "Core stability" },
                new ExerciseDTO { Name = "Bodyweight Hip Thrust", Sets = "3", Reps = "15-20", Notes = "" }
            });
            _defaultWorkoutExercises.Add("Beginner Lose Fat Day 6", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Split Squat", Sets = "3", Reps = "12-15 per leg", Notes = "" },
                new ExerciseDTO { Name = "Push-ups", Sets = "3", Reps = "10-15", Notes = "" },
                new ExerciseDTO { Name = "Standing Dumbbell Lateral Raise", Sets = "3", Reps = "12-15", Notes = "" },
                new ExerciseDTO { Name = "Crunches", Sets = "3", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "Bodyweight Plank", Sets = "3", Reps = "30 sec", Notes = "" }
            });

            // Intermediate
            _defaultWorkoutExercises.Add("Intermediate Lose Fat Day 1", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Barbell Squat", Sets = "4", Reps = "8-12", Notes = "" },
                new ExerciseDTO { Name = "Pull-ups", Sets = "3", Reps = "8-10", Notes = "Assisted if needed" },
                new ExerciseDTO { Name = "Dumbbell Bench Press", Sets = "3", Reps = "10-12", Notes = "" },
                new ExerciseDTO { Name = "Plank", Sets = "3", Reps = "45 sec", Notes = "" }
            });
            _defaultWorkoutExercises.Add("Intermediate Lose Fat Day 2", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Romanian Deadlift", Sets = "4", Reps = "8-12", Notes = "" },
                new ExerciseDTO { Name = "Push-ups", Sets = "3", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "Seated Row", Sets = "3", Reps = "10-12", Notes = "Machine or cable" },
                new ExerciseDTO { Name = "Russian Twist", Sets = "3", Reps = "20", Notes = "Core" }
            });
            _defaultWorkoutExercises.Add("Intermediate Lose Fat Day 3", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Walking Lunge", Sets = "4", Reps = "10-12 per leg", Notes = "" },
                new ExerciseDTO { Name = "Incline Dumbbell Press", Sets = "3", Reps = "10-12", Notes = "" },
                new ExerciseDTO { Name = "Lat Pulldown", Sets = "3", Reps = "10-12", Notes = "" },
                new ExerciseDTO { Name = "Side Plank", Sets = "3", Reps = "30 sec per side", Notes = "" }
            });
            _defaultWorkoutExercises.Add("Intermediate Lose Fat Day 4", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Leg Press", Sets = "4", Reps = "10-12", Notes = "" },
                new ExerciseDTO { Name = "Dips", Sets = "3", Reps = "8-10", Notes = "Assisted if needed" },
                new ExerciseDTO { Name = "Barbell Curl", Sets = "3", Reps = "10-12", Notes = "" },
                new ExerciseDTO { Name = "Mountain Climbers", Sets = "3", Reps = "20", Notes = "" }
            });
            _defaultWorkoutExercises.Add("Intermediate Lose Fat Day 5", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Front Squat", Sets = "4", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Push Press", Sets = "3", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Face Pulls", Sets = "3", Reps = "12-15", Notes = "" },
                new ExerciseDTO { Name = "Bicycle Crunch", Sets = "3", Reps = "20", Notes = "" }
            });
            _defaultWorkoutExercises.Add("Intermediate Lose Fat Day 6", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Bulgarian Split Squat", Sets = "4", Reps = "8-10 per leg", Notes = "" },
                new ExerciseDTO { Name = "Chin-ups", Sets = "3", Reps = "8-10", Notes = "Assisted if needed" },
                new ExerciseDTO { Name = "Arnold Press", Sets = "3", Reps = "10-12", Notes = "" },
                new ExerciseDTO { Name = "Leg Raises", Sets = "3", Reps = "15-20", Notes = "" }
            });

            // Advanced
            _defaultWorkoutExercises.Add("Advanced Lose Fat Day 1", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Barbell Back Squat", Sets = "5", Reps = "5-8", Notes = "Heavy" },
                new ExerciseDTO { Name = "Weighted Pull-ups", Sets = "4", Reps = "6-8", Notes = "" },
                new ExerciseDTO { Name = "Barbell Bench Press", Sets = "4", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Hanging Leg Raise", Sets = "4", Reps = "12-15", Notes = "" }
            });
            _defaultWorkoutExercises.Add("Advanced Lose Fat Day 2", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Barbell Deadlift", Sets = "5", Reps = "5-8", Notes = "Heavy" },
                new ExerciseDTO { Name = "Dumbbell Incline Press", Sets = "4", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Pendlay Row", Sets = "4", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Ab Wheel Rollout", Sets = "4", Reps = "10-12", Notes = "" }
            });
            _defaultWorkoutExercises.Add("Advanced Lose Fat Day 3", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Front Squat", Sets = "5", Reps = "5-8", Notes = "" },
                new ExerciseDTO { Name = "Weighted Dips", Sets = "4", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Barbell Curl", Sets = "4", Reps = "10-12", Notes = "" },
                new ExerciseDTO { Name = "Toes to Bar", Sets = "4", Reps = "10-12", Notes = "" }
            });
            _defaultWorkoutExercises.Add("Advanced Lose Fat Day 4", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Barbell Lunge", Sets = "4", Reps = "8-10 per leg", Notes = "" },
                new ExerciseDTO { Name = "Overhead Press", Sets = "4", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Face Pulls", Sets = "4", Reps = "12-15", Notes = "" },
                new ExerciseDTO { Name = "Russian Twist", Sets = "4", Reps = "20", Notes = "" }
            });
            _defaultWorkoutExercises.Add("Advanced Lose Fat Day 5", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Barbell Hip Thrust", Sets = "4", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Weighted Chin-ups", Sets = "4", Reps = "6-8", Notes = "" },
                new ExerciseDTO { Name = "Arnold Press", Sets = "4", Reps = "10-12", Notes = "" },
                new ExerciseDTO { Name = "Bicycle Crunch", Sets = "4", Reps = "20", Notes = "" }
            });
            _defaultWorkoutExercises.Add("Advanced Lose Fat Day 6", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Barbell Front Squat", Sets = "4", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Weighted Dips", Sets = "4", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Barbell Row", Sets = "4", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Hanging Knee Raise", Sets = "4", Reps = "15-20", Notes = "" }
            });
            {
                _defaultWorkoutExercises.Add("Full Body Workout", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Squats", Sets = "3-4", Reps = "8-12", Notes = "Warm-up sets recommended", VideoLink = "link_to_squats_video", SkillLevelName = "Beginner", ExerciseTypeName = "Strength" },
                new ExerciseDTO { Name = "Bench Press", Sets = "3-4", Reps = "8-12", Notes = "Control the eccentric", VideoLink = "link_to_bench_press_video", SkillLevelName = "Beginner", ExerciseTypeName = "Strength" },
                new ExerciseDTO { Name = "Barbell Rows", Sets = "3-4", Reps = "8-12", Notes = "Squeeze shoulder blades", VideoLink = "link_to_barbell_rows_video", SkillLevelName = "Beginner", ExerciseTypeName = "Strength" },
                new ExerciseDTO { Name = "Overhead Press", Sets = "3", Reps = "8-12", Notes = "Keep core tight", VideoLink = "link_to_overhead_press_video", SkillLevelName = "Beginner", ExerciseTypeName = "Strength" },
                new ExerciseDTO { Name = "Plank", Sets = "3", Reps = "30-60 sec", Notes = "Maintain a straight line", VideoLink = "link_to_plank_video", SkillLevelName = "Beginner", ExerciseTypeName = "Core" }
            });
                _defaultWorkoutExercises.Add("Upper Body Workout", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Push-ups", Sets = "3", Reps = "AMRAP", Notes = "Full range of motion" },
                new ExerciseDTO { Name = "Dumbbell Rows", Sets = "3-4", Reps = "10-15 per arm", Notes = "Focus on back contraction" },
                new ExerciseDTO { Name = "Overhead Dumbbell Press", Sets = "3", Reps = "10-15", Notes = "" },
                new ExerciseDTO { Name = "Bicep Curls", Sets = "3", Reps = "10-15", Notes = "" },
                new ExerciseDTO { Name = "Triceps Dips", Sets = "3", Reps = "AMRAP", Notes = "Can use a chair" }
            });
                _defaultWorkoutExercises.Add("Lower Body Workout", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Goblet Squats", Sets = "3-4", Reps = "10-15", Notes = "Keep chest up" },
                new ExerciseDTO { Name = "Lunges", Sets = "3", Reps = "10-12 per leg", Notes = "Step forward or backward" },
                new ExerciseDTO { Name = "Glute Bridges", Sets = "3", Reps = "15-20", Notes = "Squeeze glutes at the top" },
                new ExerciseDTO { Name = "Calf Raises", Sets = "3", Reps = "15-20", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Full Body A", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Deadlifts", Sets = "3", Reps = "5-8", Notes = "Proper form is crucial" },
                new ExerciseDTO { Name = "Incline Dumbbell Press", Sets = "3", Reps = "8-12", Notes = "" },
                new ExerciseDTO { Name = "Lat Pulldowns", Sets = "3", Reps = "8-12", Notes = "" },
                new ExerciseDTO { Name = "Leg Press", Sets = "3", Reps = "10-15", Notes = "" },
                new ExerciseDTO { Name = "Side Planks", Sets = "3", Reps = "30 sec per side", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Full Body B", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Barbell Squats", Sets = "3", Reps = "5-8", Notes = "" },
                new ExerciseDTO { Name = "Overhead Press", Sets = "3", Reps = "8-12", Notes = "" },
                new ExerciseDTO { Name = "Bent-Over Rows", Sets = "3", Reps = "8-12", Notes = "" },
                new ExerciseDTO { Name = "Romanian Deadlifts", Sets = "3", Reps = "8-12", Notes = "Feel stretch in hamstrings" },
                new ExerciseDTO { Name = "Russian Twists", Sets = "3", Reps = "15-20 per side", Notes = "With or without weight" }
            });
                _defaultWorkoutExercises.Add("Full Body C", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Push Press", Sets = "3", Reps = "6-10", Notes = "Use leg drive" },
                new ExerciseDTO { Name = "Pull-ups", Sets = "3", Reps = "AMRAP", Notes = "" },
                new ExerciseDTO { Name = "Dumbbell Bench Press", Sets = "3", Reps = "10-15", Notes = "" },
                new ExerciseDTO { Name = "Leg Extensions", Sets = "3", Reps = "12-15", Notes = "" },
                new ExerciseDTO { Name = "Hamstring Curls", Sets = "3", Reps = "12-15", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Chest & Triceps", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Barbell Bench Press", Sets = "3-4", Reps = "6-10", Notes = "" },
                new ExerciseDTO { Name = "Dumbbell Flyes", Sets = "3", Reps = "10-15", Notes = "" },
                new ExerciseDTO { Name = "Incline Dumbbell Press", Sets = "3", Reps = "8-12", Notes = "" },
                new ExerciseDTO { Name = "Close-Grip Bench Press", Sets = "3", Reps = "8-12", Notes = "" },
                new ExerciseDTO { Name = "Triceps Pushdowns", Sets = "3", Reps = "10-15", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Back & Biceps", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Deadlifts", Sets = "1-3", Reps = "3-5", Notes = "Heavy, focus on form" },
                new ExerciseDTO { Name = "Pull-ups", Sets = "3", Reps = "AMRAP", Notes = "" },
                new ExerciseDTO { Name = "Barbell Rows", Sets = "3", Reps = "8-12", Notes = "" },
                new ExerciseDTO { Name = "Hammer Curls", Sets = "3", Reps = "10-15", Notes = "" },
                new ExerciseDTO { Name = "Face Pulls", Sets = "3", Reps = "12-15", Notes = "For rear delts and upper back" }
            });
                _defaultWorkoutExercises.Add("Legs & Abs", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Barbell Squats", Sets = "3-4", Reps = "6-10", Notes = "" },
                new ExerciseDTO { Name = "Leg Press", Sets = "3", Reps = "10-15", Notes = "" },
                new ExerciseDTO { Name = "Leg Curls", Sets = "3", Reps = "12-15", Notes = "" },
                new ExerciseDTO { Name = "Calf Raises (standing)", Sets = "3", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "Crunches", Sets = "3", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "Leg Raises", Sets = "3", Reps = "15-20", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Shoulders & Traps", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Overhead Press (barbell or dumbbell)", Sets = "3-4", Reps = "6-10", Notes = "" },
                new ExerciseDTO { Name = "Lateral Raises", Sets = "3", Reps = "12-15", Notes = "" },
                new ExerciseDTO { Name = "Front Raises", Sets = "3", Reps = "10-15", Notes = "" },
                new ExerciseDTO { Name = "Shrugs", Sets = "3", Reps = "10-15", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Push (Chest, Shoulders, Triceps)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Bench Press", Sets = "2", Reps = "to failure (max 10)", Notes = "" },
                new ExerciseDTO { Name = "Incline Dumbbell Press", Sets = "2", Reps = "to failure (max 10)", Notes = "" },
                new ExerciseDTO { Name = "Overhead Press", Sets = "2", Reps = "to failure (max 10)", Notes = "" },
                new ExerciseDTO { Name = "Lateral Raises", Sets = "2", Reps = "to failure (max 10)", Notes = "" },
                new ExerciseDTO { Name = "Triceps Pushdowns", Sets = "2", Reps = "to failure (max 10)", Notes = "" },
                new ExerciseDTO { Name = "Overhead Triceps Extension", Sets = "2", Reps = "to failure (max 10)", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Pull (Back, Biceps)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Pull-ups", Sets = "2", Reps = "to failure (max 10)", Notes = "" },
                new ExerciseDTO { Name = "Barbell Rows", Sets = "2", Reps = "to failure (max 10)", Notes = "" },
                new ExerciseDTO { Name = "Seated Cable Rows", Sets = "2", Reps = "to failure (max 10)", Notes = "" },
                new ExerciseDTO { Name = "Face Pulls", Sets = "2", Reps = "to failure (max 10)", Notes = "" },
                new ExerciseDTO { Name = "Barbell Curls", Sets = "2", Reps = "to failure (max 10)", Notes = "" },
                new ExerciseDTO { Name = "Hammer Curls", Sets = "2", Reps = "to failure (max 10)", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Legs (Quads, Hamstrings, Glutes, Calves)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Barbell Squats", Sets = "2", Reps = "to failure (max 10)", Notes = "" },
                new ExerciseDTO { Name = "Romanian Deadlifts", Sets = "2", Reps = "to failure (max 10)", Notes = "" },
                new ExerciseDTO { Name = "Leg Press", Sets = "2", Reps = "to failure (max 10)", Notes = "" },
                new ExerciseDTO { Name = "Leg Extensions", Sets = "2", Reps = "to failure (max 10)", Notes = "" },
                new ExerciseDTO { Name = "Leg Curls", Sets = "2", Reps = "to failure (max 10)", Notes = "" },
                new ExerciseDTO { Name = "Standing Calf Raises", Sets = "2", Reps = "to failure (max 10)", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Light Cardio (30 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Treadmill (Walk/Light Jog)", Sets = "1", Reps = "30 min", Notes = "Maintain a steady, comfortable pace" } });
                _defaultWorkoutExercises.Add("Moderate Cardio (45 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Elliptical / Bike", Sets = "1", Reps = "45 min", Notes = "Moderate intensity, challenging but sustainable" } });
                _defaultWorkoutExercises.Add("Intense Cardio (60 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "HIIT (Intervals)", Sets = "1", Reps = "60 min", Notes = "Max effort sprints/work periods followed by recovery periods" } });
                _defaultWorkoutExercises.Add("Cardio Session A (30 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Cycling", Sets = "1", Reps = "30 min", Notes = "Steady state" } });
                _defaultWorkoutExercises.Add("Cardio Session B (30 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Rowing", Sets = "1", Reps = "30 min", Notes = "Moderate pace" } });
                _defaultWorkoutExercises.Add("Cardio Session C (30 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Brisk Walk", Sets = "1", Reps = "30 min", Notes = "Outdoor or treadmill" } });
                _defaultWorkoutExercises.Add("Cardio D (30 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Jump Rope", Sets = "1", Reps = "30 min", Notes = "Intervals or continuous" } });
                _defaultWorkoutExercises.Add("Cardio E (30 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Stair Climber", Sets = "1", Reps = "30 min", Notes = "Steady state" } });
                _defaultWorkoutExercises.Add("Cardio F (30 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Swimming", Sets = "1", Reps = "30 min", Notes = "Laps or water aerobics" } });
                _defaultWorkoutExercises.Add("Cardio Session A (45 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Running", Sets = "1", Reps = "45 min", Notes = "Steady pace, outdoor or treadmill" } });
                _defaultWorkoutExercises.Add("Cardio Session B (45 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Cycling (road or stationary)", Sets = "1", Reps = "45 min", Notes = "Mixed terrain or resistance" } });
                _defaultWorkoutExercises.Add("Cardio Session C (45 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Elliptical", Sets = "1", Reps = "45 min", Notes = "Vary resistance and incline" } });
                _defaultWorkoutExercises.Add("Cardio D (45 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Zumba/Dance Fitness", Sets = "1", Reps = "45 min", Notes = "High energy, fun workout" } });
                _defaultWorkoutExercises.Add("Cardio E (45 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Swimming Laps", Sets = "1", Reps = "45 min", Notes = "Focus on different strokes" } });
                _defaultWorkoutExercises.Add("Cardio F (45 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Stairmaster", Sets = "1", Reps = "45 min", Notes = "Challenging cardiovascular workout" } });
                _defaultWorkoutExercises.Add("Cardio Session A (60 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Long Distance Run", Sets = "1", Reps = "60 min", Notes = "Endurance pace" } });
                _defaultWorkoutExercises.Add("Cardio Session B (60 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "High-Intensity Interval Training (HIIT)", Sets = "1", Reps = "60 min", Notes = "Max effort sprints/work periods followed by rest" } });
                _defaultWorkoutExercises.Add("Cardio Session C (60 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Spin Class", Sets = "1", Reps = "60 min", Notes = "Guided cycling workout" } });
                _defaultWorkoutExercises.Add("Cardio D (60 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Circuit Training", Sets = "1", Reps = "60 min", Notes = "Combine cardio and strength exercises" } });
                _defaultWorkoutExercises.Add("Cardio E (60 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Cross-Country Skiing (machine)", Sets = "1", Reps = "60 min", Notes = "Full body cardio" } });
                _defaultWorkoutExercises.Add("Cardio F (60 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Boxing/Kickboxing workout", Sets = "1", Reps = "60 min", Notes = "High energy, stress-relieving cardio" } });
                _defaultWorkoutExercises.Add("Endurance Session (40 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Steady State Run/Bike", Sets = "1", Reps = "40 min", Notes = "Maintain a consistent, challenging pace" } });
                _defaultWorkoutExercises.Add("Endurance Session (60 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Longer Steady State Run/Bike", Sets = "1", Reps = "60 min", Notes = "Focus on sustaining effort" } });
                _defaultWorkoutExercises.Add("Long Endurance Session (90 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Extended Steady State Activity", Sets = "1", Reps = "90 min", Notes = "e.g., long bike ride, continuous swimming" } });
                _defaultWorkoutExercises.Add("Endurance A (40 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Brisk Walking (Outdoor)", Sets = "1", Reps = "40 min", Notes = "Incline if possible" } });
                _defaultWorkoutExercises.Add("Endurance B (40 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Elliptical Trainer", Sets = "1", Reps = "40 min", Notes = "Low impact, full body" } });
                _defaultWorkoutExercises.Add("Endurance C (40 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Cycling (Stationary)", Sets = "1", Reps = "40 min", Notes = "Vary resistance" } });
                _defaultWorkoutExercises.Add("Endurance D (40 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Stair Climbing Machine", Sets = "1", Reps = "40 min", Notes = "Steady pace" } });
                _defaultWorkoutExercises.Add("Endurance E (40 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Rowing Machine", Sets = "1", Reps = "40 min", Notes = "Consistent strokes per minute" } });
                _defaultWorkoutExercises.Add("Endurance F (40 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Swimming (Continuous)", Sets = "1", Reps = "40 min", Notes = "Focus on consistent pace" } });
                _defaultWorkoutExercises.Add("Endurance A (60 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Moderate Jogging", Sets = "1", Reps = "60 min", Notes = "Maintain target heart rate" } });
                _defaultWorkoutExercises.Add("Endurance B (60 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Longer Cycling Session", Sets = "1", Reps = "60 min", Notes = "Road or high-resistance stationary" } });
                _defaultWorkoutExercises.Add("Endurance C (60 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Hiking", Sets = "1", Reps = "60 min", Notes = "Varying terrain" } });
                _defaultWorkoutExercises.Add("Endurance D (60 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Lap Swimming", Sets = "1", Reps = "60 min", Notes = "Focus on technique and distance" } });
                _defaultWorkoutExercises.Add("Endurance E (60 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Elliptical with varied program", Sets = "1", Reps = "60 min", Notes = "Hills, intervals, etc." } });
                _defaultWorkoutExercises.Add("Endurance F (60 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Power Walking", Sets = "1", Reps = "60 min", Notes = "Fast pace, arm movement" } });
                _defaultWorkoutExercises.Add("Endurance A (90 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Long Run", Sets = "1", Reps = "90 min", Notes = "Build aerobic base" } });
                _defaultWorkoutExercises.Add("Endurance B (90 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Long Cycle Ride", Sets = "1", Reps = "90 min", Notes = "Outdoor or high-end trainer" } });
                _defaultWorkoutExercises.Add("Endurance C (90 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Trail Running/Hiking", Sets = "1", Reps = "90 min", Notes = "Uneven terrain, challenging" } });
                _defaultWorkoutExercises.Add("Endurance D (90 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Extended Rowing Session", Sets = "1", Reps = "90 min", Notes = "Consistent, moderate pace" } });
                _defaultWorkoutExercises.Add("Endurance E (90 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Cross-Training (Mix activities)", Sets = "1", Reps = "90 min", Notes = "e.g., 45 min run, 45 min bike" } });
                _defaultWorkoutExercises.Add("Endurance F (90 min)", new List<ExerciseDTO> { new ExerciseDTO { Name = "Swimming (Long Distance)", Sets = "1", Reps = "90 min", Notes = "Continuous, focused on distance" } });
                _defaultWorkoutExercises.Add("Core Workout (15 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Plank", Sets = "3", Reps = "30 sec", Notes = "" },
                new ExerciseDTO { Name = "Crunches", Sets = "3", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "Leg Raises", Sets = "3", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "Bird-Dog", Sets = "3", Reps = "10 per side", Notes = "Focus on stability" }
            });
                _defaultWorkoutExercises.Add("Core Workout (25 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Plank Variations (side, elbow)", Sets = "3", Reps = "45 sec", Notes = "" },
                new ExerciseDTO { Name = "Russian Twists", Sets = "3", Reps = "20-30 per side", Notes = "With or without weight" },
                new ExerciseDTO { Name = "Bicycle Crunches", Sets = "3", Reps = "15-20 per side", Notes = "" },
                new ExerciseDTO { Name = "Dead Bug", Sets = "3", Reps = "10-12 per side", Notes = "Slow and controlled" },
                new ExerciseDTO { Name = "Hanging Leg Raises", Sets = "3", Reps = "10-15", Notes = "If pull-up bar available" }
            });
                _defaultWorkoutExercises.Add("Intense Core Workout (35 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Weighted Plank", Sets = "3", Reps = "60 sec", Notes = "" },
                new ExerciseDTO { Name = "Cable Crunches", Sets = "3", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "Ab Rollout", Sets = "3", Reps = "10-15", Notes = "Using ab wheel or barbell" },
                new ExerciseDTO { Name = "Toes to Bar", Sets = "3", Reps = "AMRAP", Notes = "" },
                new ExerciseDTO { Name = "Dragon Flags", Sets = "3", Reps = "5-8", Notes = "Advanced move" },
                new ExerciseDTO { Name = "Medicine Ball Slams", Sets = "3", Reps = "10-15", Notes = "Explosive movement" }
            });
                _defaultWorkoutExercises.Add("Core A (15 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Crunches", Sets = "3", Reps = "20", Notes = "" },
                new ExerciseDTO { Name = "Flutter Kicks", Sets = "3", Reps = "30 sec", Notes = "" },
                new ExerciseDTO { Name = "Reverse Crunches", Sets = "3", Reps = "15", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Core B (15 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Plank", Sets = "3", Reps = "45 sec", Notes = "" },
                new ExerciseDTO { Name = "Side Plank (per side)", Sets = "3", Reps = "30 sec", Notes = "" },
                new ExerciseDTO { Name = "Leg Raises", Sets = "3", Reps = "15", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Core C (15 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Bicycle Crunches", Sets = "3", Reps = "20 per side", Notes = "" },
                new ExerciseDTO { Name = "Russian Twists", Sets = "3", Reps = "20 per side", Notes = "" },
                new ExerciseDTO { Name = "Bird-Dog", Sets = "3", Reps = "10 per side", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Core D (15 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Sit-ups", Sets = "3", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "Mountain Climbers", Sets = "3", Reps = "30 sec", Notes = "" },
                new ExerciseDTO { Name = "Heel Touches", Sets = "3", Reps = "20 per side", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Core E (15 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "V-ups", Sets = "3", Reps = "10-12", Notes = "" },
                new ExerciseDTO { Name = "Hollow Body Hold", Sets = "3", Reps = "30 sec", Notes = "" },
                new ExerciseDTO { Name = "Superman", Sets = "3", Reps = "12-15", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Core F (15 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Forearm Plank", Sets = "3", Reps = "60 sec", Notes = "" },
                new ExerciseDTO { Name = "Side Plank with Hip Dip (per side)", Sets = "3", Reps = "10-15", Notes = "" },
                new ExerciseDTO { Name = "Flutter Kicks (lying)", Sets = "3", Reps = "45 sec", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Core A (25 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Hanging Knee Raises", Sets = "3", Reps = "12-15", Notes = "" },
                new ExerciseDTO { Name = "Cable Crunches", Sets = "3", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "Bosu Ball Crunches", Sets = "3", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "Stability Ball Plank", Sets = "3", Reps = "60 sec", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Core B (25 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Weighted Russian Twists", Sets = "3", Reps = "15-20 per side", Notes = "" },
                new ExerciseDTO { Name = "Decline Crunches", Sets = "3", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "Leg Raises with Hip Lift", Sets = "3", Reps = "15", Notes = "" },
                new ExerciseDTO { Name = "Wood Chops (cable or dumbbell)", Sets = "3", Reps = "10-12 per side", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Core C (25 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Dragon Flags (assisted)", Sets = "3", Reps = "5-8", Notes = "" },
                new ExerciseDTO { Name = "Ab Rollout", Sets = "3", Reps = "10-15", Notes = "" },
                new ExerciseDTO { Name = "Side Bends (dumbbell)", Sets = "3", Reps = "15 per side", Notes = "" },
                new ExerciseDTO { Name = "Flutter Kicks (weighted ankle)", Sets = "3", Reps = "60 sec", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Core D (25 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Medicine Ball Throws (various directions)", Sets = "3", Reps = "10-12", Notes = "Explosive" },
                new ExerciseDTO { Name = "Plank Jacks", Sets = "3", Reps = "20", Notes = "" },
                new ExerciseDTO { Name = "Reverse Hyperextensions", Sets = "3", Reps = "15-20", Notes = "For lower back and glutes" },
                new ExerciseDTO { Name = "Ab Wheel Rollout", Sets = "3", Reps = "10-15", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Core E (25 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Landmine Twists", Sets = "3", Reps = "10-12 per side", Notes = "" },
                new ExerciseDTO { Name = "Weighted Sit-ups", Sets = "3", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "L-Sit Holds", Sets = "3", Reps = "Max hold (15-30 sec)", Notes = "" },
                new ExerciseDTO { Name = "Alternating Toe Touches", Sets = "3", Reps = "15-20 per side", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Core F (25 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Barbell Rollouts", Sets = "3", Reps = "8-12", Notes = "" },
                new ExerciseDTO { Name = "Pallof Press", Sets = "3", Reps = "10-12 per side", Notes = "Anti-rotation" },
                new ExerciseDTO { Name = "Hanging Windshield Wipers", Sets = "3", Reps = "8-12 per side", Notes = "" },
                new ExerciseDTO { Name = "Plate Twists (seated)", Sets = "3", Reps = "20-30 per side", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Core A (35 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Weighted Cable Crunches", Sets = "4", Reps = "15-20", Notes = "Heavy" },
                new ExerciseDTO { Name = "Hanging Leg Raises (toes to bar)", Sets = "4", Reps = "10-15", Notes = "" },
                new ExerciseDTO { Name = "Dragon Flags", Sets = "3-4", Reps = "5-8", Notes = "Full control" },
                new ExerciseDTO { Name = "Ab Wheel Rollouts (standing)", Sets = "3", Reps = "8-10", Notes = "Advanced" },
                new ExerciseDTO { Name = "Machine Crunches", Sets = "3", Reps = "20-25", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Core B (35 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Weighted Russian Twists (heavy)", Sets = "4", Reps = "15-20 per side", Notes = "" },
                new ExerciseDTO { Name = "Weighted Decline Sit-ups", Sets = "4", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "Side Planks (weighted/dynamic)", Sets = "3", Reps = "45-60 sec per side", Notes = "" },
                new ExerciseDTO { Name = "Barbell Landmine Twists", Sets = "3-4", Reps = "10-12 per side", Notes = "Explosive" },
                new ExerciseDTO { Name = "Vacuum Pose", Sets = "3", Reps = "30-60 sec hold", Notes = "For transverse abdominis" }
            });
                _defaultWorkoutExercises.Add("Core C (35 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "GHD Sit-ups", Sets = "3-4", Reps = "15-20", Notes = "If machine available" },
                new ExerciseDTO { Name = "L-Sit to V-Sit progressions", Sets = "3", Reps = "Max hold/reps", Notes = "Advanced calisthenics" },
                new ExerciseDTO { Name = "Human Flag progressions", Sets = "3", Reps = "Max hold", Notes = "Very advanced" },
                new ExerciseDTO { Name = "Cable Wood Chops (heavy)", Sets = "3-4", Reps = "10-12 per side", Notes = "" },
                new ExerciseDTO { Name = "Reverse Hyper Machine", Sets = "3", Reps = "20-25", Notes = "For lower back and glutes" }
            });
                _defaultWorkoutExercises.Add("Core D (35 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Medicine Ball Slams (explosive)", Sets = "4", Reps = "12-15", Notes = "" },
                new ExerciseDTO { Name = "Renegade Rows", Sets = "3-4", Reps = "8-12 per side", Notes = "Core stability and upper body" },
                new ExerciseDTO { Name = "Plank with Shoulder Taps (fast)", Sets = "3", Reps = "20 per side", Notes = "" },
                new ExerciseDTO { Name = "Barbell Overhead Carries", Sets = "3", Reps = "50-100 ft", Notes = "Full body stability" },
                new ExerciseDTO { Name = "Weighted Hyperextensions", Sets = "3", Reps = "15-20", Notes = "For lower back" }
            });
                _defaultWorkoutExercises.Add("Core E (35 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Turkish Get-ups (light-moderate weight)", Sets = "3", Reps = "3-5 per side", Notes = "Complex full-body core movement" },
                new ExerciseDTO { Name = "Standing Cable Crunches", Sets = "3-4", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "Barbell Twists (standing)", Sets = "3", Reps = "20-30 per side", Notes = "Use a light bar or stick" },
                new ExerciseDTO { Name = "Weighted Leg Raises (Ankle Weights)", Sets = "3", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "Stability Ball Pass (from hands to feet)", Sets = "3", Reps = "15-20", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Core F (35 min)", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Ab Rollout Wheel from Knees (Full Extension)", Sets = "4", Reps = "10-15", Notes = "" },
                new ExerciseDTO { Name = "Kettlebell Windmills", Sets = "3", Reps = "8-10 per side", Notes = "Flexibility and core stability" },
                new ExerciseDTO { Name = "Barbell Rollouts (from feet)", Sets = "3", Reps = "5-8", Notes = "Very challenging" },
                new ExerciseDTO { Name = "Front Lever Progressions", Sets = "3", Reps = "Max hold", Notes = "Gymnastics core strength" },
                new ExerciseDTO { Name = "Back Extensions (on GHD or Roman Chair)", Sets = "3", Reps = "15-20", Notes = "Target lower back" }
            });
                _defaultWorkoutExercises.Add("Rest / Active Recovery", new List<ExerciseDTO>
            {
                new ExerciseDTO { Name = "Light Stretching", Sets = "1", Reps = "15 min", Notes = "Focus on major muscle groups" },
                new ExerciseDTO { Name = "Foam Rolling", Sets = "1", Reps = "10 min", Notes = "Target sore areas" },
                new ExerciseDTO { Name = "Gentle Walk", Sets = "1", Reps = "20 min", Notes = "Low impact, promote blood flow" }
            });
                // General Fitness Days (Beginner, Intermediate, Advanced)
                // Beginner
                _defaultWorkoutExercises.Add("Beginner General Fitness Day 1", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Bodyweight Squats", Sets = "3", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "Push-ups", Sets = "3", Reps = "10-15", Notes = "Knees if needed" },
                new ExerciseDTO { Name = "Standing Overhead Dumbbell Press", Sets = "3", Reps = "10-12", Notes = "Light weight" },
                new ExerciseDTO { Name = "Plank", Sets = "3", Reps = "30 sec", Notes = "Core stability" }
            });
                _defaultWorkoutExercises.Add("Beginner General Fitness Day 2", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Walking Lunges", Sets = "3", Reps = "12-15 per leg", Notes = "" },
                new ExerciseDTO { Name = "Incline Push-ups", Sets = "3", Reps = "10-15", Notes = "" },
                new ExerciseDTO { Name = "Dumbbell Rows", Sets = "3", Reps = "12-15", Notes = "Each arm" },
                new ExerciseDTO { Name = "Glute Bridge", Sets = "3", Reps = "15-20", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Beginner General Fitness Day 3", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Step-ups", Sets = "3", Reps = "12-15 per leg", Notes = "" },
                new ExerciseDTO { Name = "Shoulder Taps", Sets = "3", Reps = "20", Notes = "Core and shoulders" },
                new ExerciseDTO { Name = "Standing Calf Raises", Sets = "3", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "Superman", Sets = "3", Reps = "15", Notes = "Lower back" }
            });
                _defaultWorkoutExercises.Add("Beginner General Fitness Day 4", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Reverse Lunges", Sets = "3", Reps = "12-15 per leg", Notes = "" },
                new ExerciseDTO { Name = "Bench Dips", Sets = "3", Reps = "10-12", Notes = "Triceps" },
                new ExerciseDTO { Name = "Standing Dumbbell Curl", Sets = "3", Reps = "12-15", Notes = "" },
                new ExerciseDTO { Name = "Side Plank", Sets = "3", Reps = "20 sec per side", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Beginner General Fitness Day 5", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Goblet Squat", Sets = "3", Reps = "12-15", Notes = "" },
                new ExerciseDTO { Name = "Standing Shoulder Press", Sets = "3", Reps = "10-12", Notes = "" },
                new ExerciseDTO { Name = "Mountain Climbers", Sets = "3", Reps = "20", Notes = "" },
                new ExerciseDTO { Name = "Bird Dog", Sets = "3", Reps = "10 per side", Notes = "Core stability" }
            });
                _defaultWorkoutExercises.Add("Beginner General Fitness Day 6", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Split Squat", Sets = "3", Reps = "12-15 per leg", Notes = "" },
                new ExerciseDTO { Name = "Push-ups", Sets = "3", Reps = "10-15", Notes = "" },
                new ExerciseDTO { Name = "Standing Dumbbell Lateral Raise", Sets = "3", Reps = "12-15", Notes = "" },
                new ExerciseDTO { Name = "Crunches", Sets = "3", Reps = "15-20", Notes = "" }
            });

                // Intermediate
                _defaultWorkoutExercises.Add("Intermediate General Fitness Day 1", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Barbell Squat", Sets = "4", Reps = "8-12", Notes = "" },
                new ExerciseDTO { Name = "Pull-ups", Sets = "3", Reps = "8-10", Notes = "Assisted if needed" },
                new ExerciseDTO { Name = "Dumbbell Bench Press", Sets = "3", Reps = "10-12", Notes = "" },
                new ExerciseDTO { Name = "Plank", Sets = "3", Reps = "45 sec", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Intermediate General Fitness Day 2", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Romanian Deadlift", Sets = "4", Reps = "8-12", Notes = "" },
                new ExerciseDTO { Name = "Push-ups", Sets = "3", Reps = "15-20", Notes = "" },
                new ExerciseDTO { Name = "Seated Row", Sets = "3", Reps = "10-12", Notes = "Machine or cable" },
                new ExerciseDTO { Name = "Russian Twist", Sets = "3", Reps = "20", Notes = "Core" }
            });
                _defaultWorkoutExercises.Add("Intermediate General Fitness Day 3", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Walking Lunge", Sets = "4", Reps = "10-12 per leg", Notes = "" },
                new ExerciseDTO { Name = "Incline Dumbbell Press", Sets = "3", Reps = "10-12", Notes = "" },
                new ExerciseDTO { Name = "Lat Pulldown", Sets = "3", Reps = "10-12", Notes = "" },
                new ExerciseDTO { Name = "Side Plank", Sets = "3", Reps = "30 sec per side", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Intermediate General Fitness Day 4", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Leg Press", Sets = "4", Reps = "10-12", Notes = "" },
                new ExerciseDTO { Name = "Dips", Sets = "3", Reps = "8-10", Notes = "Assisted if needed" },
                new ExerciseDTO { Name = "Barbell Curl", Sets = "3", Reps = "10-12", Notes = "" },
                new ExerciseDTO { Name = "Mountain Climbers", Sets = "3", Reps = "20", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Intermediate General Fitness Day 5", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Front Squat", Sets = "4", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Push Press", Sets = "3", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Face Pulls", Sets = "3", Reps = "12-15", Notes = "" },
                new ExerciseDTO { Name = "Bicycle Crunch", Sets = "3", Reps = "20", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Intermediate General Fitness Day 6", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Bulgarian Split Squat", Sets = "4", Reps = "8-10 per leg", Notes = "" },
                new ExerciseDTO { Name = "Chin-ups", Sets = "3", Reps = "8-10", Notes = "Assisted if needed" },
                new ExerciseDTO { Name = "Arnold Press", Sets = "3", Reps = "10-12", Notes = "" },
                new ExerciseDTO { Name = "Leg Raises", Sets = "3", Reps = "15-20", Notes = "" }
            });

                // Advanced
                _defaultWorkoutExercises.Add("Advanced General Fitness Day 1", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Barbell Back Squat", Sets = "5", Reps = "5-8", Notes = "Heavy" },
                new ExerciseDTO { Name = "Weighted Pull-ups", Sets = "4", Reps = "6-8", Notes = "" },
                new ExerciseDTO { Name = "Barbell Bench Press", Sets = "4", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Hanging Leg Raise", Sets = "4", Reps = "12-15", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Advanced General Fitness Day 2", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Barbell Deadlift", Sets = "5", Reps = "5-8", Notes = "Heavy" },
                new ExerciseDTO { Name = "Dumbbell Incline Press", Sets = "4", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Pendlay Row", Sets = "4", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Ab Wheel Rollout", Sets = "4", Reps = "10-12", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Advanced General Fitness Day 3", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Front Squat", Sets = "5", Reps = "5-8", Notes = "" },
                new ExerciseDTO { Name = "Weighted Dips", Sets = "4", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Barbell Curl", Sets = "4", Reps = "10-12", Notes = "" },
                new ExerciseDTO { Name = "Toes to Bar", Sets = "4", Reps = "10-12", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Advanced General Fitness Day 4", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Barbell Lunge", Sets = "4", Reps = "8-10 per leg", Notes = "" },
                new ExerciseDTO { Name = "Overhead Press", Sets = "4", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Face Pulls", Sets = "4", Reps = "12-15", Notes = "" },
                new ExerciseDTO { Name = "Russian Twist", Sets = "4", Reps = "20", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Advanced General Fitness Day 5", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Barbell Hip Thrust", Sets = "4", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Weighted Chin-ups", Sets = "4", Reps = "6-8", Notes = "" },
                new ExerciseDTO { Name = "Arnold Press", Sets = "4", Reps = "10-12", Notes = "" },
                new ExerciseDTO { Name = "Bicycle Crunch", Sets = "4", Reps = "20", Notes = "" }
            });
                _defaultWorkoutExercises.Add("Advanced General Fitness Day 6", new List<ExerciseDTO> {
                new ExerciseDTO { Name = "Barbell Front Squat", Sets = "4", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Weighted Dips", Sets = "4", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Barbell Row", Sets = "4", Reps = "8-10", Notes = "" },
                new ExerciseDTO { Name = "Hanging Knee Raise", Sets = "4", Reps = "15-20", Notes = "" }
            });
            }
        }

        private async Task<int?> GetExerciseIdFromName(string exerciseName)
        {
            var exercise = await _context.Exercises
                                         .AsNoTracking() 
                                         .FirstOrDefaultAsync(e => e.ExerciseName == exerciseName);
            return exercise?.ExerciseId;
        }
        private async Task<Exercises> GetExerciseDetailsById(int exerciseId)
        {
            return await _context.Exercises
                .Include(e => e.ExerciseMuscleTargets)
                    .ThenInclude(emt => emt.MuscleGroup)
                .Include(e => e.ExerciseMuscleTargets)
                    .ThenInclude(emt => emt.SubMuscle)
                .Include(e => e.SkillLevel)
                .Include(e => e.ExerciseType)
                .FirstOrDefaultAsync(e => e.ExerciseId == exerciseId);
        }

        public async Task<List<WorkoutTypeDTO>> GetWorkoutSchedule(string goal, string level, int workoutDays)
{
    var scheduleNames = new List<string>();
    List<WorkoutTypeDTO> fullSchedule = new List<WorkoutTypeDTO>();

    if (string.Equals(goal, "BuildStrength", StringComparison.OrdinalIgnoreCase))
    {
        if (string.Equals(level, "beginner", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Full Body Workout" }; break;
                case 2: scheduleNames = new List<string> { "Upper Body Workout", "Lower Body Workout" }; break;
                case 3: scheduleNames = new List<string> { "Full Body A", "Full Body B", "Full Body C" }; break;
                case 4: scheduleNames = new List<string> { "Upper Body Workout", "Lower Body Workout", "Upper Body Workout", "Lower Body Workout" }; break;
                case 5: scheduleNames = new List<string> { "Chest & Triceps", "Back & Biceps", "Legs & Abs", "Shoulders & Traps", "Full Body Workout" }; break;
                case 6: scheduleNames = new List<string> { "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)", "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)" }; break;
                case 7: scheduleNames = new List<string> { "Full Body Workout", "Upper Body Workout", "Lower Body Workout", "Full Body A", "Full Body B", "Full Body C", "Active Recovery" }; break;
            }
        }
        else if (string.Equals(level, "intermediate", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Full Body Workout" }; break;
                case 2: scheduleNames = new List<string> { "Upper Body Workout", "Lower Body Workout" }; break;
                case 3: scheduleNames = new List<string> { "Full Body A", "Full Body B", "Full Body C" }; break;
                case 4: scheduleNames = new List<string> { "Upper Body Workout", "Lower Body Workout", "Upper Body Workout", "Lower Body Workout" }; break;
                case 5: scheduleNames = new List<string> { "Chest & Triceps", "Back & Biceps", "Legs & Abs", "Shoulders & Traps", "Full Body Workout" }; break;
                case 6: scheduleNames = new List<string> { "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)", "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)" }; break;
                case 7: scheduleNames = new List<string> { "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)", "Upper Body Workout", "Lower Body Workout", "Full Body Workout", "Active Recovery" }; break;
            }
        }
        else if (string.Equals(level, "advanced", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Full Body Workout" }; break;
                case 2: scheduleNames = new List<string> { "Upper Body Workout", "Lower Body Workout" }; break;
                case 3: scheduleNames = new List<string> { "Full Body A", "Full Body B", "Full Body C" }; break;
                case 4: scheduleNames = new List<string> { "Upper Body Workout", "Lower Body Workout", "Upper Body Workout", "Lower Body Workout" }; break;
                case 5: scheduleNames = new List<string> { "Chest & Triceps", "Back & Biceps", "Legs & Abs", "Shoulders & Traps", "Full Body Workout" }; break;
                case 6: scheduleNames = new List<string> { "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)", "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)" }; break;
                case 7: scheduleNames = new List<string> { "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)", "Upper Body Workout", "Lower Body Workout", "Full Body Workout", "Active Recovery" }; break;
            }
        }
    }
    else if (string.Equals(goal, "BuildMuscle", StringComparison.OrdinalIgnoreCase))
    {
        if (string.Equals(level, "Beginner", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Full Body Workout" }; break;
                case 2: scheduleNames = new List<string> { "Full Body Workout", "Upper Body Workout" }; break;
                case 3: scheduleNames = new List<string> { "Full Body Workout", "Upper Body Workout", "Lower Body Workout" }; break;
                case 4: scheduleNames = new List<string> { "Full Body Workout", "Upper Body Workout", "Lower Body Workout", "Full Body Workout" }; break;
                case 5: scheduleNames = new List<string> { "Full Body Workout", "Upper Body Workout", "Lower Body Workout", "Full Body Workout", "Upper Body Workout" }; break;
                case 6: scheduleNames = new List<string> { "Full Body Workout", "Upper Body Workout", "Lower Body Workout", "Full Body Workout", "Upper Body Workout", "Lower Body Workout" }; break;
                case 7: scheduleNames = new List<string> { "Full Body Workout", "Upper Body Workout", "Lower Body Workout", "Full Body Workout", "Upper Body Workout", "Lower Body Workout", "Active Recovery" }; break;
            }
        }
        else if (string.Equals(level, "Intermediate", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Push (Chest, Shoulders, Triceps)" }; break;
                case 2: scheduleNames = new List<string> { "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)" }; break;
                case 3: scheduleNames = new List<string> { "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)" }; break;
                case 4: scheduleNames = new List<string> { "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)", "Push (Chest, Shoulders, Triceps)" }; break;
                case 5: scheduleNames = new List<string> { "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)", "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)" }; break;
                case 6: scheduleNames = new List<string> { "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)", "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)" }; break;
                case 7: scheduleNames = new List<string> { "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)", "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)", "Active Recovery" }; break;
            }
        }
        else if (string.Equals(level, "Advanced", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Push (Chest, Shoulders, Triceps)" }; break;
                case 2: scheduleNames = new List<string> { "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)" }; break;
                case 3: scheduleNames = new List<string> { "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)" }; break;
                case 4: scheduleNames = new List<string> { "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)", "Push (Chest, Shoulders, Triceps)" }; break;
                case 5: scheduleNames = new List<string> { "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)", "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)" }; break;
                case 6: scheduleNames = new List<string> { "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)", "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)" }; break;
                case 7: scheduleNames = new List<string> { "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)", "Push (Chest, Shoulders, Triceps)", "Pull (Back, Biceps)", "Legs (Quads, Hamstrings, Glutes, Calves)", "Active Recovery" }; break;
            }
        }
    }
    else if (string.Equals(goal, "LossWeight", StringComparison.OrdinalIgnoreCase))
    {
        if (string.Equals(level, "Beginner", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Beginner Lose Fat Day 1" }; break;
                case 2: scheduleNames = new List<string> { "Beginner Lose Fat Day 1", "Beginner Lose Fat Day 2" }; break;
                case 3: scheduleNames = new List<string> { "Beginner Lose Fat Day 1", "Beginner Lose Fat Day 2", "Beginner Lose Fat Day 3" }; break;
                case 4: scheduleNames = new List<string> { "Beginner Lose Fat Day 1", "Beginner Lose Fat Day 2", "Beginner Lose Fat Day 3", "Beginner Lose Fat Day 4" }; break;
                case 5: scheduleNames = new List<string> { "Beginner Lose Fat Day 1", "Beginner Lose Fat Day 2", "Beginner Lose Fat Day 3", "Beginner Lose Fat Day 4", "Beginner Lose Fat Day 5" }; break;
                case 6: scheduleNames = new List<string> { "Beginner Lose Fat Day 1", "Beginner Lose Fat Day 2", "Beginner Lose Fat Day 3", "Beginner Lose Fat Day 4", "Beginner Lose Fat Day 5", "Beginner Lose Fat Day 6" }; break;
                case 7: scheduleNames = new List<string> { "Beginner Lose Fat Day 1", "Beginner Lose Fat Day 2", "Beginner Lose Fat Day 3", "Beginner Lose Fat Day 4", "Beginner Lose Fat Day 5", "Beginner Lose Fat Day 6", "Active Recovery" }; break;
            }
        }
        else if (string.Equals(level, "Intermediate", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Intermediate Lose Fat Day 1" }; break;
                case 2: scheduleNames = new List<string> { "Intermediate Lose Fat Day 1", "Intermediate Lose Fat Day 2" }; break;
                case 3: scheduleNames = new List<string> { "Intermediate Lose Fat Day 1", "Intermediate Lose Fat Day 2", "Intermediate Lose Fat Day 3" }; break;
                case 4: scheduleNames = new List<string> { "Intermediate Lose Fat Day 1", "Intermediate Lose Fat Day 2", "Intermediate Lose Fat Day 3", "Intermediate Lose Fat Day 4" }; break;
                case 5: scheduleNames = new List<string> { "Intermediate Lose Fat Day 1", "Intermediate Lose Fat Day 2", "Intermediate Lose Fat Day 3", "Intermediate Lose Fat Day 4", "Intermediate Lose Fat Day 5" }; break;
                case 6: scheduleNames = new List<string> { "Intermediate Lose Fat Day 1", "Intermediate Lose Fat Day 2", "Intermediate Lose Fat Day 3", "Intermediate Lose Fat Day 4", "Intermediate Lose Fat Day 5", "Intermediate Lose Fat Day 6" }; break;
                case 7: scheduleNames = new List<string> { "Intermediate Lose Fat Day 1", "Intermediate Lose Fat Day 2", "Intermediate Lose Fat Day 3", "Intermediate Lose Fat Day 4", "Intermediate Lose Fat Day 5", "Intermediate Lose Fat Day 6", "Active Recovery" }; break;
            }
        }
        else if (string.Equals(level, "Advanced", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Advanced Lose Fat Day 1" }; break;
                case 2: scheduleNames = new List<string> { "Advanced Lose Fat Day 1", "Advanced Lose Fat Day 2" }; break;
                case 3: scheduleNames = new List<string> { "Advanced Lose Fat Day 1", "Advanced Lose Fat Day 2", "Advanced Lose Fat Day 3" }; break;
                case 4: scheduleNames = new List<string> { "Advanced Lose Fat Day 1", "Advanced Lose Fat Day 2", "Advanced Lose Fat Day 3", "Advanced Lose Fat Day 4" }; break;
                case 5: scheduleNames = new List<string> { "Advanced Lose Fat Day 1", "Advanced Lose Fat Day 2", "Advanced Lose Fat Day 3", "Advanced Lose Fat Day 4", "Advanced Lose Fat Day 5" }; break;
                case 6: scheduleNames = new List<string> { "Advanced Lose Fat Day 1", "Advanced Lose Fat Day 2", "Advanced Lose Fat Day 3", "Advanced Lose Fat Day 4", "Advanced Lose Fat Day 5", "Advanced Lose Fat Day 6" }; break;
                case 7: scheduleNames = new List<string> { "Advanced Lose Fat Day 1", "Advanced Lose Fat Day 2", "Advanced Lose Fat Day 3", "Advanced Lose Fat Day 4", "Advanced Lose Fat Day 5", "Advanced Lose Fat Day 6", "Active Recovery" }; break;
            }
        }
    }
    else if (string.Equals(goal, "MaintainWeight", StringComparison.OrdinalIgnoreCase))
    {
        if (string.Equals(level, "Beginner", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Beginner General Fitness Day 1" }; break;
                case 2: scheduleNames = new List<string> { "Beginner General Fitness Day 1", "Beginner General Fitness Day 2" }; break;
                case 3: scheduleNames = new List<string> { "Beginner General Fitness Day 1", "Beginner General Fitness Day 2", "Beginner General Fitness Day 3" }; break;
                case 4: scheduleNames = new List<string> { "Beginner General Fitness Day 1", "Beginner General Fitness Day 2", "Beginner General Fitness Day 3", "Beginner General Fitness Day 4" }; break;
                case 5: scheduleNames = new List<string> { "Beginner General Fitness Day 1", "Beginner General Fitness Day 2", "Beginner General Fitness Day 3", "Beginner General Fitness Day 4", "Beginner General Fitness Day 5" }; break;
                case 6: scheduleNames = new List<string> { "Beginner General Fitness Day 1", "Beginner General Fitness Day 2", "Beginner General Fitness Day 3", "Beginner General Fitness Day 4", "Beginner General Fitness Day 5", "Beginner General Fitness Day 6" }; break;
                case 7: scheduleNames = new List<string> { "Beginner General Fitness Day 1", "Beginner General Fitness Day 2", "Beginner General Fitness Day 3", "Beginner General Fitness Day 4", "Beginner General Fitness Day 5", "Beginner General Fitness Day 6", "Active Recovery" }; break;
            }
        }
        else if (string.Equals(level, "Intermediate", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Intermediate General Fitness Day 1" }; break;
                case 2: scheduleNames = new List<string> { "Intermediate General Fitness Day 1", "Intermediate General Fitness Day 2" }; break;
                case 3: scheduleNames = new List<string> { "Intermediate General Fitness Day 1", "Intermediate General Fitness Day 2", "Intermediate General Fitness Day 3" }; break;
                case 4: scheduleNames = new List<string> { "Intermediate General Fitness Day 1", "Intermediate General Fitness Day 2", "Intermediate General Fitness Day 3", "Intermediate General Fitness Day 4" }; break;
                case 5: scheduleNames = new List<string> { "Intermediate General Fitness Day 1", "Intermediate General Fitness Day 2", "Intermediate General Fitness Day 3", "Intermediate General Fitness Day 4", "Intermediate General Fitness Day 5" }; break;
                case 6: scheduleNames = new List<string> { "Intermediate General Fitness Day 1", "Intermediate General Fitness Day 2", "Intermediate General Fitness Day 3", "Intermediate General Fitness Day 4", "Intermediate General Fitness Day 5", "Intermediate General Fitness Day 6" }; break;
                case 7: scheduleNames = new List<string> { "Intermediate General Fitness Day 1", "Intermediate General Fitness Day 2", "Intermediate General Fitness Day 3", "Intermediate General Fitness Day 4", "Intermediate General Fitness Day 5", "Intermediate General Fitness Day 6", "Active Recovery" }; break;
            }
        }
        else if (string.Equals(level, "Advanced", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Advanced General Fitness Day 1" }; break;
                case 2: scheduleNames = new List<string> { "Advanced General Fitness Day 1", "Advanced General Fitness Day 2" }; break;
                case 3: scheduleNames = new List<string> { "Advanced General Fitness Day 1", "Advanced General Fitness Day 2", "Advanced General Fitness Day 3" }; break;
                case 4: scheduleNames = new List<string> { "Advanced General Fitness Day 1", "Advanced General Fitness Day 2", "Advanced General Fitness Day 3", "Advanced General Fitness Day 4" }; break;
                case 5: scheduleNames = new List<string> { "Advanced General Fitness Day 1", "Advanced General Fitness Day 2", "Advanced General Fitness Day 3", "Advanced General Fitness Day 4", "Advanced General Fitness Day 5" }; break;
                case 6: scheduleNames = new List<string> { "Advanced General Fitness Day 1", "Advanced General Fitness Day 2", "Advanced General Fitness Day 3", "Advanced General Fitness Day 4", "Advanced General Fitness Day 5", "Advanced General Fitness Day 6" }; break;
                case 7: scheduleNames = new List<string> { "Advanced General Fitness Day 1", "Advanced General Fitness Day 2", "Advanced General Fitness Day 3", "Advanced General Fitness Day 4", "Advanced General Fitness Day 5", "Advanced General Fitness Day 6", "Active Recovery" }; break;
            }
        }
    }
    else if (string.Equals(goal, "cardio", StringComparison.OrdinalIgnoreCase))
    {
        if (string.Equals(level, "beginner", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Light Cardio (30 min)" }; break;
                case 2: scheduleNames = new List<string> { "Light Cardio (30 min)", "Light Cardio (30 min)" }; break;
                case 3: scheduleNames = new List<string> { "Cardio Session A (30 min)", "Cardio Session B (30 min)", "Cardio Session C (30 min)" }; break;
                case 4: scheduleNames = new List<string> { "Cardio A (30 min)", "Cardio B (30 min)", "Cardio A (30 min)", "Cardio B (30 min)" }; break;
                case 5: scheduleNames = new List<string> { "Cardio A (30 min)", "Cardio B (30 min)", "Cardio C (30 min)", "Cardio D (30 min)", "Cardio E (30 min)" }; break;
                case 6: scheduleNames = new List<string> { "Cardio A (30 min)", "Cardio B (30 min)", "Cardio C (30 min)", "Cardio D (30 min)", "Cardio E (30 min)", "Cardio F (30 min)" }; break;
                case 7: scheduleNames = new List<string> { "Cardio A (30 min)", "Cardio B (30 min)", "Cardio C (30 min)", "Cardio D (30 min)", "Cardio E (30 min)", "Cardio F (30 min)", "Active Recovery (Light Walk)" }; break;
            }
        }
        else if (string.Equals(level, "intermediate", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Moderate Cardio (45 min)" }; break;
                case 2: scheduleNames = new List<string> { "Moderate Cardio (45 min)", "Moderate Cardio (45 min)" }; break;
                case 3: scheduleNames = new List<string> { "Cardio Session A (45 min)", "Cardio Session B (45 min)", "Cardio Session C (45 min)" }; break;
                case 4: scheduleNames = new List<string> { "Cardio A (45 min)", "Cardio B (45 min)", "Cardio A (45 min)", "Cardio B (45 min)" }; break;
                case 5: scheduleNames = new List<string> { "Cardio A (45 min)", "Cardio B (45 min)", "Cardio C (45 min)", "Cardio D (45 min)", "Cardio E (45 min)" }; break;
                case 6: scheduleNames = new List<string> { "Cardio A (45 min)", "Cardio B (45 min)", "Cardio C (45 min)", "Cardio D (45 min)", "Cardio E (45 min)", "Cardio F (45 min)" }; break;
                case 7: scheduleNames = new List<string> { "Cardio A (45 min)", "Cardio B (45 min)", "Cardio C (45 min)", "Cardio D (45 min)", "Cardio E (45 min)", "Cardio F (45 min)", "Active Recovery (Light Jog)" }; break;
            }
        }
        else if (string.Equals(level, "advanced", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Intense Cardio (60 min)" }; break;
                case 2: scheduleNames = new List<string> { "Intense Cardio (60 min)", "Intense Cardio (60 min)" }; break;
                case 3: scheduleNames = new List<string> { "Cardio Session A (60 min)", "Cardio Session B (60 min)", "Cardio Session C (60 min)" }; break;
                case 4: scheduleNames = new List<string> { "Cardio A (60 min)", "Cardio B (60 min)", "Cardio A (60 min)", "Cardio B (60 min)" }; break;
                case 5: scheduleNames = new List<string> { "Cardio A (60 min)", "Cardio B (60 min)", "Cardio C (60 min)", "Cardio D (60 min)", "Cardio E (60 min)" }; break;
                case 6: scheduleNames = new List<string> { "Cardio A (60 min)", "Cardio B (60 min)", "Cardio C (60 min)", "Cardio D (60 min)", "Cardio E (60 min)", "Cardio F (60 min)" }; break;
                case 7: scheduleNames = new List<string> { "Cardio A (60 min)", "Cardio B (60 min)", "Cardio C (60 min)", "Cardio D (60 min)", "Cardio E (60 min)", "Cardio F (60 min)", "Active Recovery (HIIT)" }; break;
            }
        }
    }
    else if (string.Equals(goal, "endurance", StringComparison.OrdinalIgnoreCase))
    {
        if (string.Equals(level, "beginner", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Endurance Session (40 min)" }; break;
                case 2: scheduleNames = new List<string> { "Endurance Session (40 min)", "Endurance Session (40 min)" }; break;
                case 3: scheduleNames = new List<string> { "Endurance A (40 min)", "Endurance B (40 min)", "Endurance C (40 min)" }; break;
                case 4: scheduleNames = new List<string> { "Endurance A (40 min)", "Endurance B (40 min)", "Endurance A (40 min)", "Endurance B (40 min)" }; break;
                case 5: scheduleNames = new List<string> { "Endurance A (40 min)", "Endurance B (40 min)", "Endurance C (40 min)", "Endurance D (40 min)", "Endurance E (40 min)" }; break;
                case 6: scheduleNames = new List<string> { "Endurance A (40 min)", "Endurance B (40 min)", "Endurance C (40 min)", "Endurance D (40 min)", "Endurance E (40 min)", "Endurance F (40 min)" }; break;
                case 7: scheduleNames = new List<string> { "Endurance A (40 min)", "Endurance B (40 min)", "Endurance C (40 min)", "Endurance D (40 min)", "Endurance E (40 min)", "Endurance F (40 min)", "Active Recovery (Light Cycling)" }; break;
            }
        }
        else if (string.Equals(level, "intermediate", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Endurance Session (60 min)" }; break;
                case 2: scheduleNames = new List<string> { "Endurance Session (60 min)", "Endurance Session (60 min)" }; break;
                case 3: scheduleNames = new List<string> { "Endurance A (60 min)", "Endurance B (60 min)", "Endurance C (60 min)" }; break;
                case 4: scheduleNames = new List<string> { "Endurance A (60 min)", "Endurance B (60 min)", "Endurance A (60 min)", "Endurance B (60 min)" }; break;
                case 5: scheduleNames = new List<string> { "Endurance A (60 min)", "Endurance B (60 min)", "Endurance C (60 min)", "Endurance D (60 min)", "Endurance E (60 min)" }; break;
                case 6: scheduleNames = new List<string> { "Endurance A (60 min)", "Endurance B (60 min)", "Endurance C (60 min)", "Endurance D (60 min)", "Endurance E (60 min)", "Endurance F (60 min)" }; break;
                case 7: scheduleNames = new List<string> { "Endurance A (60 min)", "Endurance B (60 min)", "Endurance C (60 min)", "Endurance D (60 min)", "Endurance E (60 min)", "Endurance F (60 min)", "Active Recovery (Swimming)" }; break;
            }
        }
        else if (string.Equals(level, "advanced", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Long Endurance Session (90 min)" }; break;
                case 2: scheduleNames = new List<string> { "Long Endurance Session (90 min)", "Long Endurance Session (90 min)" }; break;
                case 3: scheduleNames = new List<string> { "Endurance A (90 min)", "Endurance B (90 min)", "Endurance C (90 min)" }; break;
                case 4: scheduleNames = new List<string> { "Endurance A (90 min)", "Endurance B (90 min)", "Endurance A (90 min)", "Endurance B (90 min)" }; break;
                case 5: scheduleNames = new List<string> { "Endurance A (90 min)", "Endurance B (90 min)", "Endurance C (90 min)", "Endurance D (90 min)", "Endurance E (90 min)" }; break;
                case 6: scheduleNames = new List<string> { "Endurance A (90 min)", "Endurance B (90 min)", "Endurance C (90 min)", "Endurance D (90 min)", "Endurance E (90 min)", "Endurance F (90 min)" }; break;
                case 7: scheduleNames = new List<string> { "Endurance A (90 min)", "Endurance B (90 min)", "Endurance C (90 min)", "Endurance D (90 min)", "Endurance E (90 min)", "Endurance F (90 min)", "Active Recovery (Yoga)" }; break;
            }
        }
    }
    else if (string.Equals(goal, "core", StringComparison.OrdinalIgnoreCase))
    {
        if (string.Equals(level, "beginner", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Core Workout (15 min)" }; break;
                case 2: scheduleNames = new List<string> { "Core Workout (15 min)", "Core Workout (15 min)" }; break;
                case 3: scheduleNames = new List<string> { "Core A (15 min)", "Core B (15 min)", "Core C (15 min)" }; break;
                case 4: scheduleNames = new List<string> { "Core A (15 min)", "Core B (15 min)", "Core A (15 min)", "Core B (15 min)" }; break;
                case 5: scheduleNames = new List<string> { "Core A (15 min)", "Core B (15 min)", "Core C (15 min)", "Core D (15 min)", "Core E (15 min)" }; break;
                case 6: scheduleNames = new List<string> { "Core A (15 min)", "Core B (15 min)", "Core C (15 min)", "Core D (15 min)", "Core E (15 min)", "Core F (15 min)" }; break;
                case 7: scheduleNames = new List<string> { "Core A (15 min)", "Core B (15 min)", "Core C (15 min)", "Core D (15 min)", "Core E (15 min)", "Core F (15 min)", "Active Recovery (Stretching)" }; break;
            }
        }
        else if (string.Equals(level, "intermediate", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Core Workout (25 min)" }; break;
                case 2: scheduleNames = new List<string> { "Core Workout (25 min)", "Core Workout (25 min)" }; break;
                case 3: scheduleNames = new List<string> { "Core A (25 min)", "Core B (25 min)", "Core C (25 min)" }; break;
                case 4: scheduleNames = new List<string> { "Core A (25 min)", "Core B (25 min)", "Core A (25 min)", "Core B (25 min)" }; break;
                case 5: scheduleNames = new List<string> { "Core A (25 min)", "Core B (25 min)", "Core C (25 min)", "Core D (25 min)", "Core E (25 min)" }; break;
                case 6: scheduleNames = new List<string> { "Core A (25 min)", "Core B (25 min)", "Core C (25 min)", "Core D (25 min)", "Core E (25 min)", "Core F (25 min)" }; break;
                case 7: scheduleNames = new List<string> { "Core A (25 min)", "Core B (25 min)", "Core C (25 min)", "Core D (25 min)", "Core E (25 min)", "Core F (25 min)", "Active Recovery (Foam Rolling)" }; break;
            }
        }
        else if (string.Equals(level, "advanced", StringComparison.OrdinalIgnoreCase))
        {
            switch (workoutDays)
            {
                case 1: scheduleNames = new List<string> { "Intense Core Workout (35 min)" }; break;
                case 2: scheduleNames = new List<string> { "Intense Core Workout (35 min)", "Intense Core Workout (35 min)" }; break;
                case 3: scheduleNames = new List<string> { "Core A (35 min)", "Core B (35 min)", "Core C (35 min)" }; break;
                case 4: scheduleNames = new List<string> { "Core A (35 min)", "Core B (35 min)", "Core A (35 min)", "Core B (35 min)" }; break;
                case 5: scheduleNames = new List<string> { "Core A (35 min)", "Core B (35 min)", "Core C (35 min)", "Core D (35 min)", "Core E (35 min)" }; break;
                case 6: scheduleNames = new List<string> { "Core A (35 min)", "Core B (35 min)", "Core C (35 min)", "Core D (35 min)", "Core E (35 min)", "Core F (35 min)" }; break;
                case 7: scheduleNames = new List<string> { "Core A (35 min)", "Core B (35 min)", "Core C (35 min)", "Core D (35 min)", "Core E (35 min)", "Core F (35 min)", "Active Recovery (Pilates)" }; break;
            }
        }
    }
    int totalDays = 7;
    while (scheduleNames.Count < totalDays)
    {
        scheduleNames.Add("Rest / Active Recovery");
    }

    var allExerciseNames = new HashSet<string>();
    foreach (var dayName in scheduleNames)
    {
        if (_defaultWorkoutExercises.TryGetValue(dayName, out var exercises))
        {
            foreach (var ex in exercises)
            {
                if (!string.IsNullOrWhiteSpace(ex.Name))
                    allExerciseNames.Add(ex.Name);
            }
        }
    }

    if (!allExerciseNames.Any())
        return fullSchedule; 

    var dbExercises = new List<dynamic>();
    const int batchSize = 10;
    var allExerciseNamesList = allExerciseNames.ToList();

    for (int i = 0; i < allExerciseNamesList.Count; i += batchSize)
    {
        var batch = allExerciseNamesList.Skip(i).Take(batchSize).ToList();
        var batchResults = await _context.Exercises
            .Where(e => batch.Contains(e.ExerciseName))
            .Select(e => new {
                e.ExerciseName,
                e.ExerciseId,
                e.VideoLinks,
                SkillLevelName = e.SkillLevel.LevelName,
                ExerciseTypeName = e.ExerciseType.ExerciseTypeName
            })
            .AsNoTracking()
            .ToListAsync();
        dbExercises.AddRange(batchResults);
    }

    var dbExerciseMap = dbExercises
        .GroupBy(e => (string)e.ExerciseName, StringComparer.OrdinalIgnoreCase)
        .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);

    foreach (var dayName in scheduleNames)
    {
        if (dayName.ToLower().Contains("rest") || dayName.ToLower().Contains("active recovery"))
        {
            fullSchedule.Add(new WorkoutTypeDTO
            {
                WorkoutName = dayName,
                Exercises = new List<ExerciseDTO>() 
            });
            continue;
        }

        if (!_defaultWorkoutExercises.TryGetValue(dayName, out var exercises))
        {
            fullSchedule.Add(new WorkoutTypeDTO
            {
                WorkoutName = dayName,
                Exercises = new List<ExerciseDTO>() 
            });
            continue;
        }

        var enrichedExercises = new List<ExerciseDTO>();
        foreach (var ex in exercises)
        {
            if (dbExerciseMap.TryGetValue(ex.Name, out var dbExercise))
            {
                List<string> mainMuscles = new List<string>();
                int exerciseIdInt = 0;
                try { exerciseIdInt = (int)dbExercise.ExerciseId; } catch { }
                if (_context != null && exerciseIdInt > 0)
                {
                    var dbExerciseFull = await _context.Exercises
                        .Include(e => e.ExerciseMuscleTargets)
                            .ThenInclude(emt => emt.MuscleGroup)
                        .FirstOrDefaultAsync(e => e.ExerciseId == exerciseIdInt);
                    if (dbExerciseFull != null)
                    {
                        mainMuscles = dbExerciseFull.ExerciseMuscleTargets
                            .Where(emt => emt.IsMainMuscle && emt.MuscleGroup != null)
                            .Select(emt => emt.MuscleGroup.MuscleGroupName)
                            .Where(name => !string.IsNullOrEmpty(name))
                            .Distinct()
                            .ToList();
                    }
                }
                enrichedExercises.Add(new ExerciseDTO
                {
                    ExerciseId = exerciseIdInt,
                    Name = ex.Name,
                    Sets = ex.Sets,
                    Reps = ex.Reps,
                    Notes = ex.Notes,
                    VideoLink = dbExercise.VideoLinks,
                    SkillLevelName = dbExercise.SkillLevelName,
                    ExerciseTypeName = dbExercise.ExerciseTypeName,
                    MainMusclesTargeted = mainMuscles
                });
            }
            else
            {
                enrichedExercises.Add(ex);
            }
        }

        fullSchedule.Add(new WorkoutTypeDTO
        {
            WorkoutName = dayName,
            Exercises = enrichedExercises
        });
    }

    return fullSchedule;
}

    public async Task<List<ExerciseDTO>> GetSmartExerciseReplacement(
            int exerciseToChangeId,
            string exerciseToChangeName,
            int userSkillLevelId,
            string userFitnessGoalString,
            string userSkillLevelString,
            List<ExerciseDTO> currentWorkoutExercises,
            List<WorkoutTypeDTO> fullWorkoutPlan = null)
        {
            Exercises originalExercise = null;
            if (exerciseToChangeId > 0)
            {
                originalExercise = await GetExerciseDetailsById(exerciseToChangeId);
            }
            if (originalExercise == null && !string.IsNullOrEmpty(exerciseToChangeName))
            {
                originalExercise = await _context.Exercises
                    .Include(e => e.ExerciseMuscleTargets)
                        .ThenInclude(emt => emt.MuscleGroup)
                    .Include(e => e.SkillLevel)
                    .Include(e => e.ExerciseType)
                    .FirstOrDefaultAsync(e => e.ExerciseName == exerciseToChangeName);
            }
            if (originalExercise == null)
            {
                return new List<ExerciseDTO>();
            }


            var originalMainMuscleTargets = originalExercise.ExerciseMuscleTargets
                .Where(emt => emt.IsMainMuscle)
                .ToList();
            var originalMainMuscleGroupIds = originalMainMuscleTargets
                .Select(emt => emt.MuscleGroupId)
                .Distinct()
                .ToList();
            var originalMainSubMuscleIds = originalMainMuscleTargets
                .Select(emt => emt.SubMuscleId)
                .Distinct()
                .ToList();
            var originalMainSubMuscleNames = originalMainMuscleTargets
                .Select(emt => emt.SubMuscle?.SubMuscleName?.Trim().ToLower())
                .Where(name => !string.IsNullOrEmpty(name))
                .Distinct()
                .ToList();

            FitnessGoalMapping.FitnessGoal userFitnessGoal;
            try
            {
                userFitnessGoal = FitnessGoalMapping.ParseFitnessGoal(userFitnessGoalString);
            }
            catch (ArgumentException)
            {
                var fallback = await _context.Exercises
                    .Include(e => e.ExerciseMuscleTargets)
                        .ThenInclude(emt => emt.MuscleGroup)
                    .Include(e => e.ExerciseMuscleTargets)
                        .ThenInclude(emt => emt.SubMuscle)
                    .Include(e => e.SkillLevel)
                    .Include(e => e.ExerciseType)
                    .Where(e =>
                        e.ExerciseId != originalExercise.ExerciseId &&
                        e.SkillLevelId <= userSkillLevelId
                    )
                    .ToListAsync();
                if (fallback.Count > 0)
                {
                    var chosen = fallback.OrderBy(x => Guid.NewGuid()).First();
                    return new List<ExerciseDTO> {
                        new ExerciseDTO {
                            ExerciseId = chosen.ExerciseId,
                            Name = chosen.ExerciseName,
                            VideoLink = chosen.VideoLinks,
                            SkillLevelName = chosen.SkillLevel?.LevelName,
                            ExerciseTypeName = chosen.ExerciseType?.ExerciseTypeName,
                            Sets = "Dynamic based on goal",
                            Reps = "Auto",
                            Notes = "Fallback due to invalid goal."
                        }
                    };
                }
                var lastResort = await _context.Exercises
                    .Include(e => e.ExerciseMuscleTargets)
                        .ThenInclude(emt => emt.MuscleGroup)
                    .Include(e => e.ExerciseMuscleTargets)
                        .ThenInclude(emt => emt.SubMuscle)
                    .Include(e => e.SkillLevel)
                    .Include(e => e.ExerciseType)
                    .Where(e => e.ExerciseId != originalExercise.ExerciseId)
                    .ToListAsync();
                if (lastResort.Count > 0)
                {
                    var chosen = lastResort.OrderBy(x => Guid.NewGuid()).First();
                    return new List<ExerciseDTO> {
                        new ExerciseDTO {
                            ExerciseId = chosen.ExerciseId,
                            Name = chosen.ExerciseName,
                            VideoLink = chosen.VideoLinks,
                            SkillLevelName = chosen.SkillLevel?.LevelName,
                            ExerciseTypeName = chosen.ExerciseType?.ExerciseTypeName,
                            Sets = "Dynamic based on goal",
                            Reps = "Auto",
                            Notes = "Fallback due to invalid goal."
                        }
                    };
                }
                return new List<ExerciseDTO>();
            }
            var targetExerciseTypeNames = FitnessGoalMapping.GetExerciseTypeNamesForGoal(userFitnessGoal);
            var targetExerciseTypeIds = await _context.ExerciseTypes
                .Where(et => targetExerciseTypeNames.Contains(et.ExerciseTypeName))
                .Select(et => et.ExerciseTypeId)
                .ToListAsync();

            List<int> allowedSkillLevelIds = new List<int>();
            if (userSkillLevelId >= 3) 
            {
                allowedSkillLevelIds = new List<int> { 1, 2, 3 };
            }
            else if (userSkillLevelId == 2) 
            {
                allowedSkillLevelIds = new List<int> { 1, 2 };
            }
            else 
            {
                allowedSkillLevelIds = new List<int> { 1 };
            }



var existingExerciseIds = (currentWorkoutExercises != null)
    ? new HashSet<int>(currentWorkoutExercises.Select(e => e.ExerciseId ?? 0).Where(id => id > 0))
    : new HashSet<int>();

var originalMainMuscleGroupIdsSet = new HashSet<int>(originalMainMuscleGroupIds);
var originalMainSubMuscleIdsSet = new HashSet<int>(originalMainSubMuscleIds);
var originalMainSubMuscleNamesSet = new HashSet<string>(originalMainSubMuscleNames);


// EF Core cannot translate SequenceEqual to SQL, so we fetch a broader set and filter in memory
var allStrictDbCandidates = await _context.Exercises
    .Include(e => e.ExerciseMuscleTargets)
        .ThenInclude(emt => emt.MuscleGroup)
    .Include(e => e.ExerciseMuscleTargets)
        .ThenInclude(emt => emt.SubMuscle)
    .Include(e => e.SkillLevel)
    .Include(e => e.ExerciseType)
    .Where(e =>
        e.ExerciseId != originalExercise.ExerciseId &&
        !existingExerciseIds.Contains(e.ExerciseId) &&
        allowedSkillLevelIds.Contains(e.SkillLevelId) &&
        targetExerciseTypeIds.Contains(e.ExerciseTypeId) &&
        e.ExerciseMuscleTargets.Any(emt =>
            emt.IsMainMuscle &&
            originalMainSubMuscleIdsSet.Contains(emt.SubMuscleId) &&
            emt.IsMainMuscle == true 
        )
    )
    .ToListAsync();

// Now filter in memory for exact main muscle group match
var allStrictCandidates = allStrictDbCandidates
    .Where(e =>
        e.ExerciseMuscleTargets.Where(emt => emt.IsMainMuscle).Select(emt => emt.MuscleGroupId).Distinct().OrderBy(x => x)
            .SequenceEqual(originalMainMuscleGroupIds.OrderBy(x => x))
    ).ToList();

List<Exercises> allCandidates;
if (allStrictCandidates.Count > 0)
{
    allCandidates = allStrictCandidates;
}
else
{
    // Fallback: fetch broader set, filter in memory for exact main muscle group match
    var allDbCandidates = await _context.Exercises
        .Include(e => e.ExerciseMuscleTargets)
            .ThenInclude(emt => emt.MuscleGroup)
        .Include(e => e.ExerciseMuscleTargets)
            .ThenInclude(emt => emt.SubMuscle)
        .Include(e => e.SkillLevel)
        .Include(e => e.ExerciseType)
        .Where(e =>
            e.ExerciseId != originalExercise.ExerciseId &&
            !existingExerciseIds.Contains(e.ExerciseId) &&
            allowedSkillLevelIds.Contains(e.SkillLevelId) &&
            targetExerciseTypeIds.Contains(e.ExerciseTypeId) &&
            e.ExerciseMuscleTargets.Any(emt =>
                (emt.IsMainMuscle &&
                    (originalMainMuscleGroupIdsSet.Contains(emt.MuscleGroupId)
                     || (emt.SubMuscleId != 0 && originalMainSubMuscleIdsSet.Contains(emt.SubMuscleId))
                     || (emt.SubMuscle != null && originalMainSubMuscleNamesSet.Contains(emt.SubMuscle.SubMuscleName.Trim().ToLower()))
                    )
                )
            )
        )
        .ToListAsync();

    allCandidates = allDbCandidates
        .Where(e =>
            e.ExerciseMuscleTargets.Where(emt => emt.IsMainMuscle).Select(emt => emt.MuscleGroupId).Distinct().OrderBy(x => x)
                .SequenceEqual(originalMainMuscleGroupIds.OrderBy(x => x))
        ).ToList();
}

            var scoredCandidates = allCandidates
    .Select(e => {
        var mainTargets = e.ExerciseMuscleTargets.Where(emt => emt.IsMainMuscle).ToList();
        var mainMuscleIds = mainTargets.Select(emt => emt.MuscleGroupId).Distinct().ToList();
        var subMuscleIds = mainTargets.Select(emt => emt.SubMuscleId).Distinct().ToList();
        var subMuscleNames = mainTargets.Select(emt => emt.SubMuscle?.SubMuscleName?.Trim().ToLower())
            .Where(name => !string.IsNullOrEmpty(name)).Distinct().ToList();



        var originalMainMuscleGroupIdsSetSorted = new HashSet<int>(originalMainMuscleGroupIds);
        var mainMuscleIdsSetSorted = new HashSet<int>(mainMuscleIds);
        bool mainMuscleGroupsMatch = originalMainMuscleGroupIdsSetSorted.SetEquals(mainMuscleIdsSetSorted);
        if (!mainMuscleGroupsMatch) return null;

        int subMuscleIdOverlap = subMuscleIds.Intersect(originalMainSubMuscleIds).Count();
        int subMuscleNameOverlap = subMuscleNames.Intersect(originalMainSubMuscleNames).Count();
        int mainMuscleOverlap = mainMuscleIds.Intersect(originalMainMuscleGroupIds).Count();




        int exerciseTypeScore = (e.ExerciseTypeId == originalExercise.ExerciseTypeId) ? 10 : 0;
        int skillLevelScore = (e.SkillLevelId == originalExercise.SkillLevelId) ? 5 : 0;
        int compoundScore = (mainMuscleIds.Count > 1) ? 2 : 0;
        int notRecentlyUsedScore = (!existingExerciseIds.Contains(e.ExerciseId)) ? 3 : 0;

        int weightedScore = subMuscleNameOverlap * 100
            + subMuscleIdOverlap * 20
            + mainMuscleOverlap * 10
            + exerciseTypeScore
            + skillLevelScore
            + compoundScore
            + notRecentlyUsedScore;

        return new {
            Exercise = e,
            WeightedScore = weightedScore,
            SubMuscleNameOverlap = subMuscleNameOverlap,
            SubMuscleIdOverlap = subMuscleIdOverlap,
            MainMuscleOverlap = mainMuscleOverlap,
            MainMuscleCount = mainMuscleIds.Count,
            IsCompound = mainMuscleIds.Count > 1,
            ExerciseTypeScore = exerciseTypeScore,
            SkillLevelScore = skillLevelScore,
            NotRecentlyUsedScore = notRecentlyUsedScore
        };
    })
    .Where(x => x != null)
    .OrderByDescending(x => x.WeightedScore)
    .ThenByDescending(x => x.IsCompound)
    .ThenByDescending(x => x.MainMuscleCount)
    .ThenBy(x => Guid.NewGuid())
    .ToList();

            var bestScore = scoredCandidates.FirstOrDefault()?.WeightedScore ?? 0;
            var bestCandidates = scoredCandidates.Where(x => x.WeightedScore == bestScore && bestScore > 0).ToList();
            if (bestCandidates.Count > 0)
            {
                var chosen = bestCandidates.First();
                string note = "Smart replacement based on weighted muscle overlap.";
                if (chosen.SubMuscleNameOverlap > 0) note = "Smart replacement based on sub-muscle name overlap.";
                else if (chosen.SubMuscleIdOverlap > 0) note = "Smart replacement based on sub-muscle id overlap.";
                else if (chosen.MainMuscleOverlap > 0) note = "Smart replacement based on main muscle overlap.";
                return new List<ExerciseDTO> {
                    new ExerciseDTO {
                        ExerciseId = chosen.Exercise.ExerciseId,
                        Name = chosen.Exercise.ExerciseName,
                        VideoLink = chosen.Exercise.VideoLinks,
                        SkillLevelName = chosen.Exercise.SkillLevel?.LevelName,
                        ExerciseTypeName = chosen.Exercise.ExerciseType?.ExerciseTypeName,
                        Sets = FitnessGoalMapping.GetRecommendedSetsForGoal(userFitnessGoal, userSkillLevelString).ToString(),
                        Reps = FitnessGoalMapping.GetRecommendedRepsForGoalString(userFitnessGoal, userSkillLevelString),
                        Notes = note,
                        MainMusclesTargeted = chosen.Exercise.ExerciseMuscleTargets
                            .Where(emt => emt.IsMainMuscle)
                            .Select(emt => emt.MuscleGroup?.MuscleGroupName)
                            .Where(name => !string.IsNullOrEmpty(name))
                            .Distinct()
                            .ToList()
                    }
                };
            }
            if (allCandidates.Count == 0)
            {
                return new List<ExerciseDTO>();
            }

            if (scoredCandidates.Count >= 2)
            {
                var groupedSubName = scoredCandidates.GroupBy(x => x.SubMuscleNameOverlap).OrderByDescending(g => g.Key).ToList();
                if (groupedSubName.Count > 1)
                {
                    var secondBestGroup = groupedSubName[1].ToList();
                    var chosen = secondBestGroup.OrderBy(x => Guid.NewGuid()).First();
                    return new List<ExerciseDTO> {
                        new ExerciseDTO {
                            ExerciseId = chosen.Exercise.ExerciseId,
                            Name = chosen.Exercise.ExerciseName,
                            VideoLink = chosen.Exercise.VideoLinks,
                            SkillLevelName = chosen.Exercise.SkillLevel?.LevelName,
                            ExerciseTypeName = chosen.Exercise.ExerciseType?.ExerciseTypeName,
                            Sets = FitnessGoalMapping.GetRecommendedSetsForGoal(userFitnessGoal, userSkillLevelString).ToString(),
                            Reps = FitnessGoalMapping.GetRecommendedRepsForGoalString(userFitnessGoal, userSkillLevelString),
                            Notes = "Second-best smart replacement (sub-muscle name).",
                            MainMusclesTargeted = chosen.Exercise.ExerciseMuscleTargets
                                .Where(emt => emt.IsMainMuscle)
                                .Select(emt => emt.MuscleGroup?.MuscleGroupName)
                                .Where(name => !string.IsNullOrEmpty(name))
                                .Distinct()
                                .ToList()
                        }
                    };
                }
                var groupedSubId = scoredCandidates.GroupBy(x => x.SubMuscleIdOverlap).OrderByDescending(g => g.Key).ToList();
                if (groupedSubId.Count > 1)
                {
                    var secondBestGroup = groupedSubId[1].ToList();
                    var chosen = secondBestGroup.OrderBy(x => Guid.NewGuid()).First();
                    return new List<ExerciseDTO> {
                        new ExerciseDTO {
                            ExerciseId = chosen.Exercise.ExerciseId,
                            Name = chosen.Exercise.ExerciseName,
                            VideoLink = chosen.Exercise.VideoLinks,
                            SkillLevelName = chosen.Exercise.SkillLevel?.LevelName,
                            ExerciseTypeName = chosen.Exercise.ExerciseType?.ExerciseTypeName,
                            Sets = FitnessGoalMapping.GetRecommendedSetsForGoal(userFitnessGoal, userSkillLevelString).ToString(),
                            Reps = FitnessGoalMapping.GetRecommendedRepsForGoalString(userFitnessGoal, userSkillLevelString),
                            Notes = "Second-best smart replacement (sub-muscle id).",
                            MainMusclesTargeted = chosen.Exercise.ExerciseMuscleTargets
                                .Where(emt => emt.IsMainMuscle)
                                .Select(emt => emt.MuscleGroup?.MuscleGroupName)
                                .Where(name => !string.IsNullOrEmpty(name))
                                .Distinct()
                                .ToList()
                        }
                    };
                }
                var groupedMain = scoredCandidates.GroupBy(x => x.MainMuscleOverlap).OrderByDescending(g => g.Key).ToList();
                if (groupedMain.Count > 1)
                {
                    var secondBestGroup = groupedMain[1].ToList();
                    var chosen = secondBestGroup.OrderBy(x => Guid.NewGuid()).First();
                    return new List<ExerciseDTO> {
                        new ExerciseDTO {
                            ExerciseId = chosen.Exercise.ExerciseId,
                            Name = chosen.Exercise.ExerciseName,
                            VideoLink = chosen.Exercise.VideoLinks,
                            SkillLevelName = chosen.Exercise.SkillLevel?.LevelName,
                            ExerciseTypeName = chosen.Exercise.ExerciseType?.ExerciseTypeName,
                            Sets = FitnessGoalMapping.GetRecommendedSetsForGoal(userFitnessGoal, userSkillLevelString).ToString(),
                            Reps = FitnessGoalMapping.GetRecommendedRepsForGoalString(userFitnessGoal, userSkillLevelString),
                            Notes = "Second-best smart replacement (main muscle).",
                            MainMusclesTargeted = chosen.Exercise.ExerciseMuscleTargets
                                .Where(emt => emt.IsMainMuscle)
                                .Select(emt => emt.MuscleGroup?.MuscleGroupName)
                                .Where(name => !string.IsNullOrEmpty(name))
                                .Distinct()
                                .ToList()
                        }
                    };
                }
                if (scoredCandidates.Count > 1)
                {
                    var chosen = scoredCandidates[1];
                    return new List<ExerciseDTO> {
                        new ExerciseDTO {
                            ExerciseId = chosen.Exercise.ExerciseId,
                            Name = chosen.Exercise.ExerciseName,
                            VideoLink = chosen.Exercise.VideoLinks,
                            SkillLevelName = chosen.Exercise.SkillLevel?.LevelName,
                            ExerciseTypeName = chosen.Exercise.ExerciseType?.ExerciseTypeName,
                            Sets = FitnessGoalMapping.GetRecommendedSetsForGoal(userFitnessGoal, userSkillLevelString).ToString(),
                            Reps = FitnessGoalMapping.GetRecommendedRepsForGoalString(userFitnessGoal, userSkillLevelString),
                            Notes = "Second-best smart replacement (fallback).",
                            MainMusclesTargeted = chosen.Exercise.ExerciseMuscleTargets
                                .Where(emt => emt.IsMainMuscle)
                                .Select(emt => emt.MuscleGroup?.MuscleGroupName)
                                .Where(name => !string.IsNullOrEmpty(name))
                                .Distinct()
                                .ToList()
                        }
                    };
                }
            }

            if (scoredCandidates.Count == 1)
            {
                var chosen = scoredCandidates[0];
                return new List<ExerciseDTO> {
                    new ExerciseDTO {
                        ExerciseId = chosen.Exercise.ExerciseId,
                        Name = chosen.Exercise.ExerciseName,
                        VideoLink = chosen.Exercise.VideoLinks,
                        SkillLevelName = chosen.Exercise.SkillLevel?.LevelName,
                        ExerciseTypeName = chosen.Exercise.ExerciseType?.ExerciseTypeName,
                        Sets = FitnessGoalMapping.GetRecommendedSetsForGoal(userFitnessGoal, userSkillLevelString).ToString(),
                        Reps = FitnessGoalMapping.GetRecommendedRepsForGoalString(userFitnessGoal, userSkillLevelString),
                        Notes = "Only one smart replacement found.",
                        MainMusclesTargeted = chosen.Exercise.ExerciseMuscleTargets
                            .Where(emt => emt.IsMainMuscle)
                            .Select(emt => emt.MuscleGroup?.MuscleGroupName)
                            .Where(name => !string.IsNullOrEmpty(name))
                            .Distinct()
                            .ToList()
                    }
                };
            }

            var fallbackCandidates = await _context.Exercises
                .Include(e => e.ExerciseMuscleTargets)
                    .ThenInclude(emt => emt.MuscleGroup)
                .Include(e => e.ExerciseMuscleTargets)
                    .ThenInclude(emt => emt.SubMuscle)
                .Include(e => e.SkillLevel)
                .Include(e => e.ExerciseType)
                .Where(e =>
                    e.ExerciseId != originalExercise.ExerciseId &&
                    e.SkillLevelId <= userSkillLevelId
                )
                .ToListAsync();
            if (fallbackCandidates.Count > 0)
            {
                var chosen = fallbackCandidates.OrderBy(x => Guid.NewGuid()).First();
                return new List<ExerciseDTO> {
                    new ExerciseDTO {
                        ExerciseId = chosen.ExerciseId,
                        Name = chosen.ExerciseName,
                        VideoLink = chosen.VideoLinks,
                        SkillLevelName = chosen.SkillLevel?.LevelName,
                        ExerciseTypeName = chosen.ExerciseType?.ExerciseTypeName,
                        Sets = FitnessGoalMapping.GetRecommendedSetsForGoal(userFitnessGoal, userSkillLevelString).ToString(),
                        Reps = FitnessGoalMapping.GetRecommendedRepsForGoalString(userFitnessGoal, userSkillLevelString),
                        Notes = "Fallback smart replacement."
                    }
                };
            }

            var lastResort2 = await _context.Exercises
                .Include(e => e.ExerciseMuscleTargets)
                    .ThenInclude(emt => emt.MuscleGroup)
                .Include(e => e.ExerciseMuscleTargets)
                    .ThenInclude(emt => emt.SubMuscle)
                .Include(e => e.SkillLevel)
                .Include(e => e.ExerciseType)
                .Where(e => e.ExerciseId != originalExercise.ExerciseId)
                .ToListAsync();
            if (lastResort2.Count > 0)
            {
                var chosen = lastResort2.OrderBy(x => Guid.NewGuid()).First();
                return new List<ExerciseDTO> {
                    new ExerciseDTO {
                        ExerciseId = chosen.ExerciseId,
                        Name = chosen.ExerciseName,
                        VideoLink = chosen.VideoLinks,
                        SkillLevelName = chosen.SkillLevel?.LevelName,
                        ExerciseTypeName = chosen.ExerciseType?.ExerciseTypeName,
                        Sets = FitnessGoalMapping.GetRecommendedSetsForGoal(userFitnessGoal, userSkillLevelString).ToString(),
                        Reps = FitnessGoalMapping.GetRecommendedRepsForGoalString(userFitnessGoal, userSkillLevelString),
                        Notes = "Last resort smart replacement."
                    }
                };
            }
            return new List<ExerciseDTO>();
        }

        async Task<List<ExerciseDTO>> IWorkoutService.GetSmartExerciseReplacement(
            int exerciseToChangeId,
            string exerciseToChangeName,
            int userSkillLevelId,
            string userFitnessGoalString,
            List<ExerciseDTO> currentWorkoutExercises,
            List<WorkoutTypeDTO> fullWorkoutPlan)
        {
            string userSkillLevelString = null;
            if (currentWorkoutExercises != null && currentWorkoutExercises.Any())
            {
                userSkillLevelString = currentWorkoutExercises
                    .Select(e => e.SkillLevelName)
                    .FirstOrDefault(s => !string.IsNullOrEmpty(s));
            }
            if (string.IsNullOrEmpty(userSkillLevelString))
            {
                userSkillLevelString = "Intermediate"; 
            }
            return await GetSmartExerciseReplacement(
                exerciseToChangeId,
                exerciseToChangeName,
                userSkillLevelId,
                userFitnessGoalString,
                userSkillLevelString,
                currentWorkoutExercises,
                fullWorkoutPlan);
        }

        public string GetRecommendedRepsDescription(string userFitnessGoalString)
        {
            FitnessGoalMapping.FitnessGoal userFitnessGoal;
            try
            {
                userFitnessGoal = FitnessGoalMapping.ParseFitnessGoal(userFitnessGoalString);
                return FitnessGoalMapping.GetRecommendedRepsForGoal(userFitnessGoal).Description;
            }
            catch (ArgumentException)
            {
                return "Unknown goal reps.";
            }
        }
    }
}