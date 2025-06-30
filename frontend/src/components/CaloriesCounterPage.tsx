import React, { useEffect, useState, useCallback, useRef } from 'react';
import { z, ZodIssue } from 'zod';

function cn(...classes: (string | undefined | null | false)[]): string {
  return classes.filter(Boolean).join(' ');
}

const calorieFormSchema = z.object({
  gender: z.enum(['Male', 'Female']),
  age: z.number().min(15, "Age must be at least 15").max(100, "Age must be at most 100"),
  height: z.number().min(100, "Height must be at least 100 cm").max(250, "Height must be at most 250 cm"),
  weight: z.number().min(30, "Weight must be at least 30 kg").max(200, "Weight must be at most 200 kg"),
  activityLevel: z.enum(['sedentary', 'lightly active', 'moderately active', 'very active', 'extra active']),
  goal: z.enum(['loseweight', 'maintainweight', 'gainweight', 'gainmuscle']),
});

type CalorieFormData = z.infer<typeof calorieFormSchema>;

interface CalorieResult {
  totalCalories: number;
  protein: number;
  carbohydrates: number;
  fats: number;
}

const FireIcon = ({ className = "w-6 h-6" }) => (
  <svg className={className} viewBox="0 0 24 24" fill="currentColor">
    <path d="M13.5.67s.74 2.65.74 4.8c0 2.06-1.35 3.73-3.41 3.73-2.07 0-3.63-1.67-3.63-3.73l.03-.36C5.21 7.51 4 10.62 4 14c0 4.42 3.58 8 8 8s8-3.58 8-8C20 8.61 17.41 3.8 13.5.67zM11.71 19c-1.78 0-3.22-1.4-3.22-3.14 0-1.62 1.05-2.76 2.81-3.12 1.77-.36 3.6-1.21 4.62-2.58.39 1.29.59 2.65.59 4.04 0 2.65-2.15 4.8-4.8 4.8z"/>
  </svg>
);

const PersonIcon = ({ className = "w-5 h-5" }) => (
  <svg className={className} viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
    <path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"/>
    <circle cx="12" cy="7" r="4"/>
  </svg>
);

const CalendarIcon = ({ className = "w-5 h-5" }) => (
  <svg className={className} viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
    <rect x="3" y="4" width="18" height="18" rx="2" ry="2"/>
    <line x1="16" y1="2" x2="16" y2="6"/>
    <line x1="8" y1="2" x2="8" y2="6"/>
    <line x1="3" y1="10" x2="21" y2="10"/>
  </svg>
);

const RulerIcon = ({ className = "w-5 h-5" }) => (
  <svg className={className} viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
    <path d="M21.5 7 19 4.5 4.5 19 7 21.5l14.5-14.5z"/>
    <path d="m6 14 4-4"/>
    <path d="m10 18 4-4"/>
    <path d="m14 22 4-4"/>
  </svg>
);

const ScaleIcon = ({ className = "w-5 h-5" }) => (
  <svg className={className} viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
    <path d="M16 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"/>
    <circle cx="12" cy="7" r="4"/>
    <path d="M6 13a6 6 0 0 1 6-6 6 6 0 0 1 6 6"/>
  </svg>
);

const ActivityIcon = ({ className = "w-5 h-5" }) => (
  <svg className={className} viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
    <path d="M22 12h-4l-3 9L9 3l-3 9H2"/>
  </svg>
);

const TargetIcon = ({ className = "w-5 h-5" }) => (
  <svg className={className} viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
    <circle cx="12" cy="12" r="10"/>
    <circle cx="12" cy="12" r="6"/>
    <circle cx="12" cy="12" r="2"/>
  </svg>
);

const SparkleIcon = ({ className = "w-4 h-4" }) => (
  <svg className={className} viewBox="0 0 24 24" fill="currentColor">
    <path d="M12 0L14.59 8.41L23 11L14.59 13.59L12 22L9.41 13.59L1 11L9.41 8.41L12 0Z"/>
  </svg>
);

const LoadingIcon = ({ className = "w-6 h-6" }) => (
  <svg className={`${className} animate-spin`} viewBox="0 0 24 24" fill="none">
    <circle cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4" strokeOpacity="0.2"/>
    <path fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"/>
  </svg>
);

const FloatingElements = () => (
  <div className="absolute inset-0 overflow-hidden pointer-events-none">
    {[...Array(6)].map((_, i) => (
      <div
        key={i}
        className="absolute animate-float opacity-20"
        style={{
          left: `${Math.random() * 100}%`,
          top: `${Math.random() * 100}%`,
          animationDelay: `${Math.random() * 3}s`,
          animationDuration: `${3 + Math.random() * 2}s`
        }}
      >
        <SparkleIcon className="w-3 h-3 text-purple-400" />
      </div>
    ))}
  </div>
);

const Card: React.FC<React.HTMLAttributes<HTMLDivElement>> = ({ className, children, ...props }) => (
  <div className={cn("rounded-2xl border border-gray-800/50 text-white shadow-2xl overflow-hidden relative", className)} {...props}>
    <div className="absolute inset-0 bg-gradient-to-br from-purple-900/20 via-transparent to-blue-900/20 pointer-events-none" />
    <div className="relative z-10">
      {children}
    </div>
  </div>
);

const CardContent: React.FC<React.HTMLAttributes<HTMLDivElement>> = ({ className, children, ...props }) => (
  <div className={cn("p-6 md:p-8", className)} {...props}>
    {children}
  </div>
);

interface FormInputProps extends React.InputHTMLAttributes<HTMLInputElement> {
  label: string;
  error?: string;
  icon?: React.ReactNode;
}

const FormInput = React.forwardRef<HTMLInputElement, FormInputProps>(({ label, id, error, icon, ...props }, ref) => {
  const [isFocused, setIsFocused] = useState(false);
  
  return (
    <div className="w-full group">
      <label htmlFor={id} className="block text-sm font-medium text-gray-300 mb-3 font-courier-prime transition-colors duration-200 group-hover:text-purple-300">
        <div className="flex items-center gap-2">
          {icon && <span className="text-purple-400">{icon}</span>}
          {label}
        </div>
      </label>
      <div className="relative">
        <input
          id={id}
          ref={ref}
          onFocus={() => setIsFocused(true)}
          onBlur={() => setIsFocused(false)}
          className={cn(
            "w-full px-4 py-3 bg-gray-800/60 border border-gray-700 rounded-xl text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-purple-500 transition-all duration-300 hover:border-gray-600",
            "focus:bg-gray-800/80 focus:shadow-lg focus:shadow-purple-500/20",
            error && "border-red-500 focus:ring-red-500",
            isFocused && "transform scale-[1.02]"
          )}
          {...props}
        />
        <div className={cn(
          "absolute inset-0 rounded-xl pointer-events-none transition-opacity duration-300",
          "bg-gradient-to-r from-purple-500/10 to-blue-500/10",
          isFocused ? "opacity-100" : "opacity-0"
        )} />
      </div>
      {error && (
        <div className="mt-2 text-xs text-red-400 font-sans animate-slideDown flex items-center gap-1">
          <svg className="w-3 h-3" viewBox="0 0 24 24" fill="currentColor">
            <path d="M12 2L1 21h22L12 2zm0 3.99L19.53 19H4.47L12 5.99zM11 16h2v2h-2v-2zm0-6h2v4h-2v-4z"/>
          </svg>
          {error}
        </div>
      )}
    </div>
  );
});

interface FormSelectProps extends React.SelectHTMLAttributes<HTMLSelectElement> {
  label: string;
  error?: string;
  children: React.ReactNode;
  icon?: React.ReactNode;
}

const FormSelect = React.forwardRef<HTMLSelectElement, FormSelectProps>(({ label, id, error, children, icon, ...props }, ref) => {
  const [isFocused, setIsFocused] = useState(false);
  
  return (
    <div className="w-full group">
      <label htmlFor={id} className="block text-sm font-medium text-gray-300 mb-3 font-courier-prime transition-colors duration-200 group-hover:text-purple-300">
        <div className="flex items-center gap-2">
          {icon && <span className="text-purple-400">{icon}</span>}
          {label}
        </div>
      </label>
      <div className="relative">
        <select
          id={id}
          ref={ref}
          onFocus={() => setIsFocused(true)}
          onBlur={() => setIsFocused(false)}
          className={cn(
            "w-full px-4 py-3 bg-gray-800/60 border border-gray-700 rounded-xl text-white focus:outline-none focus:ring-2 focus:ring-purple-500 transition-all duration-300 appearance-none hover:border-gray-600",
            "focus:bg-gray-800/80 focus:shadow-lg focus:shadow-purple-500/20",
            "pr-10", // Added padding-right to make space for the custom arrow
            error && "border-red-500 focus:ring-red-500",
            isFocused && "transform scale-[1.02]"
          )}
          {...props}
        >
          {children}
        </select>
        {/* Custom SVG arrow positioned absolutely */}
        <div className="pointer-events-none absolute inset-y-0 right-0 flex items-center pr-3 text-purple-400">
          <svg className="w-5 h-5" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
            <path fillRule="evenodd" d="M5.23 7.21a.75.75 0 011.06.02L10 10.94l3.71-3.71a.75.75 0 111.06 1.06l-4.25 4.25a.75.75 0 01-1.06 0L5.21 8.27a.75.75 0 01.02-1.06z" clipRule="evenodd" />
          </svg>
        </div>
        <div className={cn(
          "absolute inset-0 rounded-xl pointer-events-none transition-opacity duration-300",
          "bg-gradient-to-r from-purple-500/10 to-blue-500/10",
          isFocused ? "opacity-100" : "opacity-0"
        )} />
      </div>
      {error && (
        <div className="mt-2 text-xs text-red-400 font-sans animate-slideDown flex items-center gap-1">
          <svg className="w-3 h-3" viewBox="0 0 24 24" fill="currentColor">
            <path d="M12 2L1 21h22L12 2zm0 3.99L19.53 19H4.47L12 5.99zM11 16h2v2h-2v-2zm0-6h2v4h-2v-4z"/>
          </svg>
          {error}
        </div>
      )}
    </div>
  );
});

export default function CaloriesCounterPage() {
  const [formData, setFormData] = useState<Partial<Omit<CalorieFormData, 'age' | 'height' | 'weight'> & { age?: number | ''; height?: number | ''; weight?: number | ''}>>({
    gender: 'Male',
    activityLevel: 'moderately active',
    goal: 'maintainweight',
  });
  const [errors, setErrors] = useState<Record<string, string>>({});
  const [calorieResult, setCalorieResult] = useState<CalorieResult | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [apiError, setApiError] = useState<string | null>(null);
  const [activeSection, setActiveSection] = useState('home');
  const [showResults, setShowResults] = useState(false);

  const sectionIds = ['home', 'calorie-calc'];

  useEffect(() => {
    const observer = new IntersectionObserver(
      (entries) => {
        entries.forEach((entry) => {
          if (entry.isIntersecting && sectionIds.includes(entry.target.id)) {
            setActiveSection(entry.target.id);
          }
        });
      },
      {
        root: null,
        rootMargin: '-50% 0px -50% 0px',
        threshold: 0,
      }
    );

    sectionIds.forEach(id => {
      const section = document.getElementById(id);
      if (section) observer.observe(section);
    });

    return () => {
      sectionIds.forEach(id => {
        const section = document.getElementById(id);
        if (section) observer.unobserve(section);
      });
    };
  }, [sectionIds]);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    const isNumberField = ['age', 'height', 'weight'].includes(name);
    setFormData((prev) => ({ ...prev, [name]: isNumberField ? (value === '' ? '' : parseFloat(value)) : value }));
    if (errors[name]) {
      setErrors(prev => {
        const newErrors = { ...prev };
        delete newErrors[name];
        return newErrors;
      });
    }
  };

  const handleCalculateCalories = async (e: React.FormEvent) => {
    e.preventDefault();
    setCalorieResult(null);
    setApiError(null);
    setShowResults(false);

    const result = calorieFormSchema.safeParse(formData);
    if (!result.success) {
      const fieldErrors: Record<string, string> = {};
      result.error.errors.forEach((err: ZodIssue) => {
        if (err.path[0]) {
          fieldErrors[err.path[0] as string] = err.message;
        }
      });
      setErrors(fieldErrors);
      return;
    }
    setErrors({});
    const validData = result.data;
    try {
      setIsLoading(true); // Start loading
      const backendUrl = process.env.NEXT_PUBLIC_BACKEND_URL;

      if (!backendUrl) {
        throw new Error("Backend URL is not configured. Please ensure NEXT_PUBLIC_BACKEND_URL is set in your .env.local file.");
      }

      const apiUrl = `${backendUrl.endsWith('/') ? backendUrl : backendUrl + '/'}api/Calories/calculate`;
      
      console.log('Attempting to call API at:', apiUrl);
      console.log('Sending data:', validData);
      
      const response = await fetch(apiUrl, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(validData),
      });

      if (!response.ok) {
        const errorText = await response.text();
        let errorMessage = `Server responded with status ${response.status}.`;
        try {
            const errorJson = JSON.parse(errorText);
            if (errorJson.message) {
                errorMessage = errorJson.message;
            } else if (errorJson.errors) {
                errorMessage = Object.values(errorJson.errors).flat().join('; ');
            } else {
                errorMessage = errorText;
            }
        } catch {
            errorMessage = errorText;
        }
        throw new Error(`API Error: ${errorMessage}. Please check your input and try again.`);
      }
      
      const data: CalorieResult = await response.json();
      console.log('API response data:', data);
      if (!data || typeof data.totalCalories === 'undefined') {
        throw new Error('Invalid response format from server');
      }

      setCalorieResult(data);
      setShowResults(true);
    } catch (error) {
      console.error('Calculation error:', error);
      setApiError(error instanceof Error ? error.message : 'An unexpected error occurred during calculation.');
    } finally {
      setIsLoading(false);
    }
  };
  
  const getGoalText = (goal: string | undefined) => {
    switch (goal) {
        case 'loseweight': return 'lose weight';
        case 'gainweight': return 'gain weight';
        case 'gainmuscle': return 'gain muscle';
        case 'maintainweight': return 'maintain your weight';
        default: return 'achieve your goal';
    }
  };

  const getGoalEmoji = (goal: string | undefined) => {
    switch (goal) {
        case 'loseweight': return '🎯';
        case 'gainweight': return '📈';
        case 'gainmuscle': return '💪';
        case 'maintainweight': return '⚖️';
        default: return '🎯';
    }
  };

  return (
    <>
      <style jsx>{`
        @keyframes float {
          0%, 100% { transform: translateY(0px) rotate(0deg); }
          50% { transform: translateY(-10px) rotate(180deg); }
        }
        @keyframes slideDown {
          from { opacity: 0; transform: translateY(-10px); }
          to { opacity: 1; transform: translateY(0); }
        }
        @keyframes pulse {
          0%, 100% { opacity: 1; }
          50% { opacity: 0.5; }
        }
        @keyframes shimmer {
          0% { background-position: -200% 0; }
          100% { background-position: 200% 0; }
        }
        .animate-float {
          animation: float 3s ease-in-out infinite;
        }
        .animate-slideDown {
          animation: slideDown 0.3s ease-out;
        }
        .animate-pulse {
          animation: pulse 2s infinite;
        }
        .animate-shimmer {
          background: linear-gradient(90deg, transparent, rgba(168, 85, 247, 0.4), transparent);
          background-size: 200% 100%;
          animation: shimmer 2s infinite;
        }
      `}</style>
      
      <div className="bg-[#070808] text-white font-inter w-full min-h-screen antialiased flex flex-col items-center justify-center pt-[70px] py-10 px-4 relative overflow-hidden">
        <FloatingElements />

        <section
          id="home"
          className="relative h-[60vh] md:h-[70vh] w-full mb-12 overflow-hidden"
        >
          <img
            src="CaloriesInterFace.png"
            alt="Calories Background"
            className="absolute inset-0 w-full h-full object-contain object-center z-0"
          />

          <div className="relative z-10 flex flex-col justify-center items-center h-full text-center px-6">
            
            <h1 className="text-5xl md:text-7xl lg:text-8xl leading-tight font-libre-baskerville font-bold text-white mb-4 animate-pulse">
              Calories Counter
            </h1>
            <p className="font-courier-prime font-normal text-lg md:text-xl text-gray-300 max-w-2xl mx-auto opacity-90">
              Fuel your ambition. Calculate your daily calorie needs to perfectly match your fitness goals.
            </p>
            <div className="mb-6 animate-bounce">
              <FireIcon className="w-16 h-16 text-purple-400 mx-auto" />
            </div>
          </div>
        </section>


        <main id="calorie-calc" className="relative z-20 -mt-[15vh] md:-mt-[20vh] w-full max-w-4xl">
          <Card className="mx-auto border-purple-500/30 shadow-2xl shadow-purple-900/40 hover:shadow-purple-900/60 transition-all duration-500">
            <CardContent>
              <form onSubmit={handleCalculateCalories} className="space-y-8">
                <div className="grid grid-cols-1 md:grid-cols-2 gap-x-8 gap-y-8">
                  <FormSelect
                    label="Gender"
                    name="gender"
                    value={formData.gender}
                    onChange={handleInputChange}
                    error={errors.gender}
                    icon={<PersonIcon />}
                  >
                    <option value="Male">Male</option>
                    <option value="Female">Female</option>
                  </FormSelect>
                  
                  <FormInput
                    label="Age"
                    name="age"
                    type="number"
                    placeholder="e.g., 25"
                    value={formData.age || ''}
                    onChange={handleInputChange}
                    error={errors.age}
                    icon={<CalendarIcon />}
                  />
                  
                  <FormInput
                    label="Height (cm)"
                    name="height"
                    type="number"
                    placeholder="e.g., 180"
                    value={formData.height || ''}
                    onChange={handleInputChange}
                    error={errors.height}
                    icon={<RulerIcon />}
                  />
                  
                  <FormInput
                    label="Weight (kg)"
                    name="weight"
                    type="number"
                    placeholder="e.g., 75"
                    value={formData.weight || ''}
                    onChange={handleInputChange}
                    error={errors.weight}
                    icon={<ScaleIcon />}
                  />
                  
                  <FormSelect
                    label="Activity Level"
                    name="activityLevel"
                    value={formData.activityLevel}
                    onChange={handleInputChange}
                    error={errors.activityLevel}
                    icon={<ActivityIcon />}
                  >
                    <option value="sedentary">Sedentary (little or no exercise)</option>
                    <option value="lightly active">Lightly Active (1-3 days/week)</option>
                    <option value="moderately active">Moderately Active (3-5 days/week)</option>
                    <option value="very active">Very Active (6-7 days/week)</option>
                    <option value="extra active">Extra Active (daily hard exercise/physical job)</option>
                  </FormSelect>
                  
                  <FormSelect
                    label="Your Goal"
                    name="goal"
                    value={formData.goal}
                    onChange={handleInputChange}
                    error={errors.goal}
                    icon={<TargetIcon />}
                  >
                    <option value="loseweight">Lose Weight</option>
                    <option value="maintainweight">Maintain Weight</option>
                    <option value="gainweight">Gain Weight</option>
                    <option value="gainmuscle">Gain Muscle</option>
                  </FormSelect>
                </div>
                
                <div className="pt-6 flex justify-center">
                  <button
                    type="submit"
                    disabled={isLoading}
                    className={cn(
                      "relative w-full md:w-auto px-12 py-4 bg-gradient-to-r from-purple-600 to-blue-600 text-white font-bold font-courier-prime text-lg rounded-xl shadow-lg transition-all duration-300 focus:outline-none focus:ring-2 focus:ring-purple-500 focus:ring-offset-2 focus:ring-offset-gray-900 overflow-hidden group",
                      "hover:from-purple-700 hover:to-blue-700 hover:shadow-xl hover:shadow-purple-500/25 hover:scale-105",
                      "disabled:from-purple-800 disabled:to-blue-800 disabled:cursor-not-allowed disabled:scale-100"
                    )}
                  >
                    <div className="absolute inset-0 bg-gradient-to-r from-white/0 via-white/20 to-white/0 translate-x-[-100%] group-hover:translate-x-[100%] transition-transform duration-1000" />
                    <div className="relative flex items-center justify-center gap-3">
                      <FireIcon className="w-5 h-5" />
                      {isLoading ? <LoadingIcon className="w-5 h-5 mr-2" /> : 'Calculate'}
                    </div>
                  </button>
                </div>
              </form>
              {apiError && (
                <div className="text-center mt-8 font-sans text-lg text-red-400 animate-slideDown flex items-center justify-center gap-2">
                  <svg className="w-5 h-5" viewBox="0 0 24 24" fill="currentColor">
                    <path d="M12 2L1 21h22L12 2zm0 3.99L19.53 19H4.47L12 5.99zM11 16h2v2h-2v-2zm0-6h2v4h-2v-4z"/>
                  </svg>
                  {apiError}
                </div>
              )}

              {calorieResult && !apiError && (
                <div className={cn(
                  "mt-10 pt-8 border-t border-gray-700/50 text-center transition-all duration-700 transform",
                  showResults ? "opacity-100 translate-y-0" : "opacity-0 translate-y-10"
                )}>
                  <div className="mb-8">
                    <div className="text-6xl mb-4">{getGoalEmoji(formData.goal)}</div>
                    <p className="font-courier-prime text-lg text-gray-300 mb-2">
                        Your estimated daily needs to {getGoalText(formData.goal)}:
                    </p>
                    <div className="relative inline-block">
                      <p className="text-5xl md:text-6xl font-bold text-transparent bg-clip-text bg-gradient-to-r from-purple-400 via-pink-400 to-blue-400 font-libre-baskerville tracking-tight relative z-10">
                        {calorieResult.totalCalories ? Math.ceil(calorieResult.totalCalories).toLocaleString() : 'Calculating...'}
                      </p>
                      <div className="absolute -inset-2 bg-gradient-to-r from-purple-500/20 to-blue-500/20 rounded-lg blur-xl animate-pulse" />
                    </div>
                    <p className="font-sans text-2xl text-gray-400 mb-8 mt-4">
                        kcal / day
                    </p>
                  </div>
                  
                  {/* Enhanced Macronutrient breakdown */}
                  <div className="grid grid-cols-1 sm:grid-cols-3 gap-8 pt-6 border-t border-gray-800/50">
                    <div className={cn(
                      "flex flex-col items-center p-6 rounded-xl bg-gradient-to-br from-green-900/30 to-green-800/20 border border-green-700/30 transition-all duration-500 hover:scale-105",
                      showResults ? "animate-slideDown" : ""
                    )} style={{ animationDelay: '0.1s' }}>
                      <div className="w-12 h-12 bg-green-500/20 rounded-full flex items-center justify-center mb-3">
                        <svg className="w-6 h-6 text-green-400" viewBox="0 0 24 24" fill="currentColor">
                          <path d="M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z"/>
                        </svg>
                      </div>
                      <span className="text-lg text-gray-400 font-courier-prime mb-2">Protein</span>
                      <span className="text-3xl font-bold text-white font-sans">{Math.ceil(calorieResult.protein)}g</span>
                      <div className="w-full bg-gray-700 rounded-full h-2 mt-3">
                        <div className="bg-green-400 h-2 rounded-full transition-all duration-1000" style={{ width: '100%' }} />
                      </div>
                    </div>
                    
                    <div className={cn(
                      "flex flex-col items-center p-6 rounded-xl bg-gradient-to-br from-orange-900/30 to-orange-800/20 border border-orange-700/30 transition-all duration-500 hover:scale-105",
                      showResults ? "animate-slideDown" : ""
                    )} style={{ animationDelay: '0.2s' }}>
                      <div className="w-12 h-12 bg-orange-500/20 rounded-full flex items-center justify-center mb-3">
                        <svg className="w-6 h-6 text-orange-400" viewBox="0 0 24 24" fill="currentColor">
                          <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm-2 15l-5-5 1.41-1.41L10 14.17l7.59-7.59L19 8l-9 9z"/>
                        </svg>
                      </div>
                      <span className="text-lg text-gray-400 font-courier-prime mb-2">Carbs</span>
                      <span className="text-3xl font-bold text-white font-sans">{Math.ceil(calorieResult.carbohydrates)}g</span>
                      <div className="w-full bg-gray-700 rounded-full h-2 mt-3">
                        <div className="bg-orange-400 h-2 rounded-full transition-all duration-1000" style={{ width: '100%' }} />
                      </div>
                    </div>
                    
                    <div className={cn(
                      "flex flex-col items-center p-6 rounded-xl bg-gradient-to-br from-yellow-900/30 to-yellow-800/20 border border-yellow-700/30 transition-all duration-500 hover:scale-105",
                      showResults ? "animate-slideDown" : ""
                    )} style={{ animationDelay: '0.3s' }}>
                      <div className="w-12 h-12 bg-yellow-500/20 rounded-full flex items-center justify-center mb-3">
                        <svg className="w-6 h-6 text-yellow-400" viewBox="0 0 24 24" fill="currentColor">
                          <path d="M12 2l.09.03L12 2l.09.03c3.61.48 6.68 2.65 8.34 5.76l.56 1.03c.25.49.37 1.03.37 1.57v.61c0 .54-.12 1.08-.37 1.57l-.56 1.03c-1.66 3.11-4.73 5.28-8.34 5.76L12 22l-.09-.03c-3.61-.48-6.68-2.65-8.34-5.76l-.56-1.03C2.76 14.68 2.64 14.14 2.64 13.6v-.61c0-.54.12-1.08.37-1.57l.56-1.03C5.23 7.28 8.3 5.11 11.91 4.63L12 2z"/>
                        </svg>
                      </div>
                      <span className="text-lg text-gray-400 font-courier-prime mb-2">Fats</span>
                      <span className="text-3xl font-bold text-white font-sans">{Math.ceil(calorieResult.fats)}g</span>
                      <div className="w-full bg-gray-700 rounded-full h-2 mt-3">
                        <div className="bg-yellow-400 h-2 rounded-full transition-all duration-1000" style={{ width: '100%' }} />
                      </div>
                    </div>
                  </div>
                  
                  {/* Success message with animation */}
                  <div className={cn(
                    "mt-8 p-4 bg-gradient-to-r from-green-900/30 to-blue-900/30 border border-green-500/30 rounded-xl transition-all duration-700",
                    showResults ? "opacity-100 translate-y-0" : "opacity-0 translate-y-5"
                  )} style={{ animationDelay: '0.4s' }}>
                    <div className="flex items-center justify-center gap-2 text-green-400">
                      <svg className="w-5 h-5" viewBox="0 0 24 24" fill="currentColor">
                        <path d="M9 16.17L4.83 12l-1.42 1.41L9 19 21 7l-1.41-1.41z"/>
                      </svg>
                      <span className="font-courier-prime">Calculation Complete!</span>
                    </div>
                    <p className="text-sm text-gray-400 mt-2 text-center">
                      These values are estimates based on your input. Consult with a nutritionist for personalized advice.
                    </p>
                  </div>
                </div>
              )}
            </CardContent>
          </Card>
        </main>
        
        {calorieResult && (
          <div className="fixed bottom-8 right-8 z-30">
            <button
              onClick={() => {
                setCalorieResult(null);
                setShowResults(false);
                document.getElementById('calorie-calc')?.scrollIntoView({ behavior: 'smooth' });
              }}
              className="w-14 h-14 bg-gradient-to-r from-purple-600 to-blue-600 rounded-full shadow-lg hover:shadow-xl transition-all duration-300 hover:scale-110 flex items-center justify-center group"
            >
              <svg className="w-6 h-6 text-white transition-transform duration-300 group-hover:rotate-180" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                <path d="M1 4v6h6"/>
                <path d="M3.51 15a9 9 0 1 0 2.13-9.36L1 10"/>
              </svg>
            </button>
          </div>
        )}
      </div>
    </>
  );
}