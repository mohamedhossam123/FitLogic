// Re-import necessary components and types
import React, { useState, useEffect, Fragment,useRef } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import AOS from 'aos';
import 'aos/dist/aos.css';
import { Transition } from '@headlessui/react';
import { XMarkIcon, ChevronDownIcon, ArrowPathIcon } from '@heroicons/react/24/solid';

// SVG Icon Components (assuming they are defined in the same file or imported)
const FireIcon = ({ className = "w-6 h-6" }) => (
    <svg className={className} viewBox="0 0 24 24" fill="currentColor">
        <path d="M13.5.67s.74 2.65.74 4.8c0 2.06-1.35 3.73-3.41 3.73-2.07 0-3.63-1.67-3.63-3.73l.03-.36C5.21 7.51 4 10.62 4 14c0 4.42 3.58 8 8 8s8-3.58 8-8C20 8.61 17.41 3.8 13.5.67zM11.71 19c-1.78 0-3.22-1.4-3.22-3.14 0-1.62 1.05-2.76 2.81-3.12 1.77-.36 3.6-1.21 4.62-2.58.39 1.29.59 2.65.59 4.04 0 2.65-2.15 4.8-4.8 4.8z"/>
    </svg>
);
const CalendarIcon = ({ className = "w-5 h-5" }) => (
    <svg className={className} viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
        <rect x="3" y="4" width="18" height="18" rx="2" ry="2"/>
        <line x1="16" y1="2" x2="16" y2="6"/><line x1="8" y1="2" x2="8" y2="6"/><line x1="3" y1="10" x2="21" y2="10"/>
    </svg>
);
const ActivityIcon = ({ className = "w-5 h-5" }) => (
    <svg className={className} viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
        <path d="M22 12h-4l-3 9L9 3l-3 9H2"/>
    </svg>
);
const TargetIcon = ({ className = "w-5 h-5" }) => (
    <svg className={className} viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
        <circle cx="12" cy="12" r="10"/><circle cx="12" cy="12" r="6"/><circle cx="12" cy="12" r="2"/>
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
const HeartIcon = ({ className = "w-6 h-6" }) => (
    <svg className={className} fill="currentColor" viewBox="0 0 24 24">
        <path fillRule="evenodd" d="M12 5.253c-3.155-4.708-10-1.503-10 5.517 0 5.378 8.161 9.947 10 12.23s10-6.852 10-12.23c0-7.02-6.845-10.225-10-5.517Z" clipRule="evenodd" />
    </svg>
);
const BoltIcon = ({ className = "w-6 h-6" }) => (
    <svg className={className} fill="currentColor" viewBox="0 0 24 24">
        <path fillRule="evenodd" d="M11.97 2.25A.75.75 0 0 0 11.25 3v7.5H4.872a.75.75 0 0 0-.547 1.284L11.75 21.75a.75.75 0 0 0 1.054-.364l1.5-4.5a.75.75 0 0 0 .023-.09l.343-1.715.009-.044a.75.75 0 0 1 .585-.56l1.92-.41L21.75 14.25a.75.75 0 0 0 .364-1.054l-9-12a.75.75 0 0 0-.844-.946Z" clipRule="evenodd" />
    </svg>
);


// Utility function
function cn(...classes: (string | undefined | null | false)[]): string {
    return classes.filter(Boolean).join(' ');
}

// Interfaces
interface Exercise {
    exerciseId: number; name: string; sets: string; reps: string; notes: string; videoLink: string; skillLevelName: string; exerciseTypeName: string; mainMusclesTargeted: string[];
}
interface WorkoutDay {
    workoutName: string; exercises: Exercise[];
}
interface WorkoutPlan {
    workoutDays: WorkoutDay[];
}

// Child Components
const DaySlider: React.FC<{ value: number; onChange: (value: number) => void; icon: React.ReactNode; }> = ({ value, onChange, icon }) => (
    <div className="w-full font-courier-prime" data-aos="fade-up">
        <label className="text-sm font-medium text-gray-400 mb-3 flex items-center gap-2">
            {icon} Workout Days: <span className="font-bold text-lg text-purple-400">{value}</span>
        </label>
        <input
            type="range"
            min="1"
            max="7"
            value={value}
            onChange={(e) => onChange(Number(e.target.value))}
            className="w-full h-2 bg-gray-800 rounded-lg appearance-none cursor-pointer range-lg accent-purple-500 focus:outline-none focus:ring-2 focus:ring-purple-500"
        />
    </div>
);

// CustomDropdown component
const CustomDropdown: React.FC<{ label: string; options: string[]; value: string; onChange: (value: string) => void; icon: React.ReactNode; }> = ({ label, options, value, onChange, icon }) => {
    const [isOpen, setIsOpen] = useState(false);
    const dropdownRef = useRef<HTMLDivElement>(null); // Create a ref for the dropdown container

    // Close dropdown when clicking outside
    useEffect(() => {
        const handleClickOutside = (event: MouseEvent) => {
            if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
                setIsOpen(false);
            }
        };
        document.addEventListener('mousedown', handleClickOutside);
        return () => {
            document.removeEventListener('mousedown', handleClickOutside);
        };
    }, [isOpen]);

    const handleSelect = (option: string) => {
        onChange(option);
        setIsOpen(false);
    };

    return (
        <div className="relative font-courier-prime w-full" data-aos="fade-up" data-aos-delay="200" ref={dropdownRef}> {/* Attach ref here */}
            <label className="text-sm font-medium text-gray-400 mb-2 flex items-center gap-2">
                {icon} {label}
            </label>
            <button type="button" onClick={() => setIsOpen(!isOpen)}
                className="relative w-full cursor-pointer rounded-xl border border-gray-800 bg-gray-900 py-3 pl-4 pr-10 text-left text-white shadow-sm focus:outline-none focus:ring-2 focus:ring-purple-500 sm:text-sm"
            >
                <span className="block truncate">{value}</span>
                <span className="pointer-events-none absolute inset-y-0 right-0 flex items-center pr-2">
                    <ChevronDownIcon className={`h-5 w-5 text-gray-400 transition-transform ${isOpen ? 'rotate-180' : ''}`} />
                </span>
            </button>
            <Transition show={isOpen} as={Fragment} leave="transition ease-in duration-100" leaveFrom="opacity-100" leaveTo="opacity-0">
                {/* Changed z-index from 50 to 60 to ensure it's on top of other potential elements like buttons */}
                <ul className="absolute z-60 mt-1 max-h-60 w-full overflow-auto rounded-md bg-gray-900 border border-gray-800 py-1 text-base shadow-lg ring-1 ring-black ring-opacity-5 sm:text-sm">
                    {options.map((option) => (
                        <li key={option} onClick={() => handleSelect(option)} className="relative cursor-default select-none py-2 pl-10 pr-4 text-gray-300 hover:bg-purple-600 hover:text-white">
                            {option}
                        </li>
                    ))}
                </ul>
            </Transition>
        </div>
    );
};

const Card: React.FC<React.HTMLAttributes<HTMLDivElement>> = ({ className, children, ...props }) => (
    <div className={cn("rounded-2xl border border-gray-800/50 text-white shadow-2xl overflow-hidden relative", className)} {...props}>
        <div className="absolute inset-0 bg-gradient-to-br from-purple-900/20 via-transparent to-blue-900/20 pointer-events-none" />
        <div className="relative z-10">{children}</div>
    </div>
);

const CardContent: React.FC<React.HTMLAttributes<HTMLDivElement>> = ({ className, children, ...props }) => (
    <div className={cn("p-6 md:p-8", className)} {...props}>{children}</div>
);

const FloatingElements = () => (
    <div className="absolute inset-0 overflow-hidden pointer-events-none">
        {[...Array(6)].map((_, i) => (
            <div key={i} className="absolute animate-float opacity-20" style={{ left: `${Math.random() * 100}%`, top: `${Math.random() * 100}%`, animationDelay: `${Math.random() * 3}s`, animationDuration: `${3 + Math.random() * 2}s` }}>
                <SparkleIcon className="w-3 h-3 text-purple-400" />
            </div>
        ))}
    </div>
);

// Main Page Component
const WorkoutPlannerPage = (): JSX.Element => {
    // showInputs state is no longer needed as inputs will always be visible
    const [days, setDays] = useState<number>(3);
    const [goal, setGoal] = useState<string>('BuildMuscle');
    const [level, setLevel] = useState<string>('Intermediate');
    const [workoutPlan, setWorkoutPlan] = useState<WorkoutPlan | null>(null);
    const [selectedWorkout, setSelectedWorkout] = useState<WorkoutDay | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        AOS.init({ duration: 1000, once: true, easing: 'ease-out-cubic' });
    }, []);

    const handleGeneratePlan = async () => {
        setIsLoading(true);
        setError(null);
        setWorkoutPlan(null);
        setSelectedWorkout(null);

        try {
            const backendUrl = process.env.NEXT_PUBLIC_BACKEND_URL;
            if (!backendUrl) { throw new Error("Backend URL is not configured."); }


// API expects: { WorkoutDays, Goal, Level } (PascalCase)
const apiUrl = `${backendUrl.replace(/\/?$/, '')}/api/Workout/generate-plan`;
const response = await fetch(apiUrl, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
        WorkoutDays: days,
        Goal: goal,
        Level: level
    }),
});


            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || 'Failed to generate workout plan.');
            }

            const data: WorkoutPlan = await response.json();
            setWorkoutPlan(data);
            if (data.workoutDays.length > 0) {
                setSelectedWorkout(data.workoutDays[0]);
            }
        } catch (err) {
            console.error("Error generating workout plan:", err);
            setError(err instanceof Error ? err.message : "An unknown error occurred.");
        } finally {
            setIsLoading(false);
        }
    };

    const handleReset = () => {
        setWorkoutPlan(null);
        setSelectedWorkout(null);
        setDays(3);
        setGoal('BuildMuscle');
        setLevel('Intermediate');
        setError(null);
    };

    return (
        <motion.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.8, ease: "easeOut" }}
            className="bg-[#070808] text-white font-inter w-full min-h-screen antialiased flex flex-col items-center pt-[70px] py-10 px-4 relative overflow-hidden"
        >
            <FloatingElements />

            <section id="workout-home" className="relative h-[60vh] md:h-[70vh] w-full mb-12 overflow-hidden flex flex-col justify-center items-center text-center px-6">
                <img src="/WorkoutPlannerBG.png" alt="Workout Background" className="absolute inset-0 w-full h-full object-contain object-center z-0" />
                <div className="relative z-10">
                    <h1 className="text-5xl md:text-7xl lg:text-8xl leading-tight font-libre-baskerville font-bold text-white mb-4 animate-pulse">
                        Workout Planner
                    </h1>
                    <p className="font-courier-prime font-normal text-lg md:text-xl text-gray-300 max-w-2xl mx-auto opacity-90">
                        Design your ultimate fitness journey. Generate personalized workout plans tailored to your goals and schedule.
                    </p>
                    <div className="mb-6 animate-bounce">
                        <BoltIcon className="w-16 h-16 text-blue-400 mx-auto" />
                    </div>
                </div>
            </section>

            <main id="workout-planner" className="relative z-20 -mt-[15vh] md:-mt-[20vh] w-full max-w-4xl">
                <Card className="mx-auto border-blue-500/30 shadow-2xl shadow-blue-900/40 hover:shadow-blue-900/60 transition-all duration-500">
                    <CardContent>
                        <h2 className="text-3xl md:text-4xl font-libre-baskerville font-bold text-center text-white mb-8">
                            Create Your Workout Plan
                        </h2>

                        <AnimatePresence mode="wait">
                            <motion.div
                                key="form-inputs"
                                initial={{ opacity: 0, y: 30 }}
                                animate={{ opacity: 1, y: 0 }}
                                exit={{ opacity: 0, y: -30 }}
                                transition={{ duration: 0.6, ease: 'easeOut' }}
                            >
                                <div className="space-y-6">
                                    <div className="px-2">
                                        <DaySlider
                                            value={days}
                                            onChange={setDays}
                                            icon={<CalendarIcon className="w-5 h-5 text-purple-400" />}
                                        />
                                    </div>
                                    <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                                        <CustomDropdown
                                            label="Goal"
                                            options={['BuildMuscle', 'LossWeight', 'BuildStrength', 'MaintainWeight']}
                                            value={goal}
                                            onChange={setGoal}
                                            icon={<TargetIcon className="w-5 h-5 text-purple-400" />}
                                        />
                                        <CustomDropdown
                                            label="Skill Level"
                                            options={['Beginner', 'Intermediate', 'Advanced']}
                                            value={level}
                                            onChange={setLevel}
                                            icon={<ActivityIcon className="w-5 h-5 text-purple-400" />}
                                        />
                                    </div>
                                </div>
                                <div className="flex justify-center gap-4 mt-8">
                                    <button
                                        onClick={handleGeneratePlan}
                                        disabled={isLoading}
                                        className={cn(
                                            "relative w-full md:w-auto px-8 py-3 bg-gradient-to-r from-blue-600 to-teal-600 text-white font-bold font-courier-prime text-lg rounded-xl shadow-lg transition-all duration-300 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 focus:ring-offset-gray-900 overflow-hidden group",
                                            "hover:from-blue-700 hover:to-teal-700 hover:shadow-xl hover:shadow-blue-500/25 hover:scale-105",
                                            "disabled:from-blue-800 disabled:to-teal-800 disabled:cursor-not-allowed disabled:scale-100"
                                        )}
                                    >
                                        <div className="absolute inset-0 bg-gradient-to-r from-white/0 via-white/20 to-white/0 translate-x-[-100%] group-hover:translate-x-[100%] transition-transform duration-1000" />
                                        <div className="relative flex items-center justify-center gap-2">
                                            {isLoading ? <LoadingIcon className="w-5 h-5" /> : <FireIcon className="w-5 h-5" />}
                                            <span>{isLoading ? 'Generating...' : 'Generate My Plan'}</span>
                                        </div>
                                    </button>
                                    {(workoutPlan || error) && (
                                        <button
                                            onClick={handleReset}
                                            className="relative w-full md:w-auto px-8 py-3 bg-gray-700 text-white font-bold font-courier-prime text-lg rounded-xl shadow-lg transition-all duration-300 focus:outline-none focus:ring-2 focus:ring-gray-500 focus:ring-offset-2 focus:ring-offset-gray-900 overflow-hidden group hover:bg-gray-600 hover:scale-105"
                                        >
                                            <div className="relative flex items-center justify-center gap-2">
                                                <ArrowPathIcon className="w-5 h-5" />
                                                Reset
                                            </div>
                                        </button>
                                    )}
                                </div>
                            </motion.div>
                        </AnimatePresence>

                        {error && (
                            <div className="text-center mt-8 font-sans text-lg text-red-400 animate-slideDown flex items-center justify-center gap-2" data-aos="fade-up">
                                <svg className="w-5 h-5" viewBox="0 0 24 24" fill="currentColor"><path d="M12 2L1 21h22L12 2zm0 3.99L19.53 19H4.47L12 5.99zM11 16h2v2h-2v-2zm0-6h2v4h-2v-4z"/></svg>
                                {error}
                            </div>
                        )}

                        <AnimatePresence>
                            {workoutPlan && (
                                <motion.div initial={{ opacity: 0, y: 50 }} animate={{ opacity: 1, y: 0 }} exit={{ opacity: 0, y: -50 }} transition={{ duration: 0.6, ease: "easeOut" }} className="mt-12 pt-8 border-t border-gray-700/50">
                                    <h3 className="text-3xl font-libre-baskerville font-bold text-center text-white mb-6">
                                        Your Personalized Workout Plan
                                    </h3>
                                    <div className="flex justify-center mb-6 flex-wrap gap-3">
                                        {workoutPlan.workoutDays.map((day, index) => (
                                            <motion.button key={index} onClick={() => setSelectedWorkout(day)} className={cn("px-6 py-2 rounded-full text-sm font-courier-prime transition-all duration-300", selectedWorkout?.workoutName === day.workoutName ? "bg-purple-600 text-white shadow-md transform scale-105" : "bg-gray-800 text-gray-300 hover:bg-gray-700 hover:text-white")} initial={{ opacity: 0, scale: 0.8 }} animate={{ opacity: 1, scale: 1 }} transition={{ delay: 0.1 * index }}>
                                                {day.workoutName}
                                            </motion.button>
                                        ))}
                                    </div>
                                    {selectedWorkout && (
                                        <motion.div key={selectedWorkout.workoutName} initial={{ opacity: 0, x: -50 }} animate={{ opacity: 1, x: 0 }} exit={{ opacity: 0, x: 50 }} transition={{ duration: 0.5, ease: "easeOut" }} className="bg-gray-900/50 p-6 rounded-xl border border-gray-700/50 shadow-inner shadow-purple-900/20">
                                            <h4 className="text-2xl font-libre-baskerville font-bold text-white mb-6 text-center">
                                                {selectedWorkout.workoutName}
                                            </h4>
                                            <ul className="space-y-6">
                                                {selectedWorkout.exercises.map((exercise) => (
                                                    <motion.li key={exercise.exerciseId} className="bg-gray-800/60 p-5 rounded-lg border border-gray-700 hover:border-purple-600 transition-all duration-300 transform hover:scale-[1.02] flex flex-col sm:flex-row items-start sm:items-center justify-between gap-4" initial={{ opacity: 0, y: 20 }} animate={{ opacity: 1, y: 0 }} transition={{ duration: 0.3, delay: 0.05 }}>
                                                        <div className="flex-1">
                                                            <h5 className="text-xl font-courier-prime font-semibold text-purple-400 mb-1 flex items-center gap-2">
                                                                <HeartIcon className="w-5 h-5 text-red-400" />
                                                                {exercise.name}
                                                            </h5>
                                                            <p className="text-gray-300 text-sm">
                                                                <span className="font-bold">Sets:</span> {exercise.sets} | <span className="font-bold">Reps:</span> {exercise.reps}
                                                            </p>
                                                            {exercise.notes && <p className="text-gray-400 text-xs mt-1 italic">{exercise.notes}</p>}
                                                            <div className="flex flex-wrap gap-2 mt-2">
                                                                <span className="bg-gray-700 text-gray-300 px-3 py-1 rounded-full text-xs font-medium">{exercise.skillLevelName}</span>
                                                                <span className="bg-gray-700 text-gray-300 px-3 py-1 rounded-full text-xs font-medium">{exercise.exerciseTypeName}</span>
                                                                {exercise.mainMusclesTargeted.map((muscle, idx) => (
                                                                    <span key={idx} className="bg-blue-800/40 text-blue-300 px-3 py-1 rounded-full text-xs font-medium">{muscle}</span>
                                                                ))}
                                                            </div>
                                                        </div>
                                                        {exercise.videoLink && (
                                                            <a href={exercise.videoLink} target="_blank" rel="noopener noreferrer" className="flex items-center gap-2 px-4 py-2 bg-purple-600 text-white rounded-lg text-sm font-medium hover:bg-purple-700 transition-colors duration-200">
                                                                <svg className="w-4 h-4" fill="currentColor" viewBox="0 0 24 24"><path d="M21 8.25c0-2.485-2.015-4.5-4.5-4.5S12 5.765 12 8.25V9h.75A.75.75 0 0 1 13.5 9.75v3.5a.75.75 0 0 1-.75.75h-.75v.75c0 2.485-2.015 4.5-4.5 4.5S3 17.235 3 14.75V14H2.25a.75.75 0 0 1-.75-.75v-3.5a.75.75 0 0 1 .75-.75h.75V8.25c0-2.485-2.015-4.5-4.5-4.5S0 5.765 0 8.25V14.5c0 2.485 2.015 4.5 4.5 4.5S9 17.235 9 14.75V14h.75a.75.75 0 0 1 .75.75v3.5a.75.75 0 0 1-.75.75H9v.75c0 2.485 2.015 4.5 4.5 4.5s4.5-2.015 4.5-4.5V14.5c0-2.485 2.015-4.5 4.5-4.5s4.5 2.015 4.5 4.5v-6.25c0-2.485-2.015-4.5-4.5-4.5ZM9 8.25h.75V9H9V8.25Z" /></svg>
                                                                Watch Video
                                                            </a>
                                                        )}
                                                    </motion.li>
                                                ))}
                                            </ul>
                                        </motion.div>
                                    )}
                                </motion.div>
                            )}
                        </AnimatePresence>
                    </CardContent>
                </Card>
            </main>
        </motion.div>
    );
};

export default WorkoutPlannerPage;