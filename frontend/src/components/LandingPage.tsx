'use client';

import React, { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';
import { motion } from 'framer-motion';
import AOS from 'aos';
import 'aos/dist/aos.css';
import ProfileCard from './ProfileCard';
import Navbar from './Navbar';
import { gsap } from 'gsap'; // Import GSAP
import { ScrollTrigger } from 'gsap/dist/ScrollTrigger'; // Import ScrollTrigger for scroll-based animations (optional)

// Register ScrollTrigger plugin
// This is important if you plan to use scroll-based animations with GSAP
gsap.registerPlugin(ScrollTrigger);

const Card: React.FC<React.HTMLAttributes<HTMLDivElement>> = ({ className, children, ...props }) => (
  <div className={`rounded-xl border border-gray-800 bg-gradient-to-br from-gray-900 to-black text-white shadow-2xl overflow-hidden ${className}`} {...props}>
    {children}
  </div>
);

const CardContent: React.FC<React.HTMLAttributes<HTMLDivElement>> = ({ className, children, ...props }) => (
  <div className={`p-8 ${className}`} {...props}>
    {children}
  </div>
);

export const LandingPage = (): JSX.Element => {
  const [activeSection, setActiveSection] = useState('home');
  const sectionIds = ['home', 'workout', 'calorie-calc', 'about'];
  const router = useRouter();

  useEffect(() => {
    // AOS Initialization
    AOS.init({
      duration: 1000,
      once: true,
      easing: 'ease-out-cubic',
    });

    // Intersection Observer for active section (keep this as is)
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

    // --- GSAP Animations ---

    // Example 1: Hero Section - Animate elements using GSAP for more control
    // You can remove data-aos from these elements if you want GSAP to fully control them
    gsap.fromTo("#home h1",
      { opacity: 0, x: -100 },
      { opacity: 1, x: 0, duration: 1.5, ease: "power3.out", delay: 0.5 }
    );

    gsap.fromTo("#home p",
      { opacity: 0, x: 100 },
      { opacity: 1, x: 0, duration: 1.5, ease: "power3.out", delay: 0.7 }
    );

    // Example 2: Workout Section - Scroll-triggered animation for the "Elevate Your Fitness Journey" text
    gsap.fromTo("#workout h2",
      { opacity: 0, y: 50 },
      {
        opacity: 1,
        y: 0,
        duration: 1,
        ease: "power2.out",
        scrollTrigger: {
          trigger: "#workout h2",
          start: "top 80%", // When the top of the trigger hits 80% of the viewport
          end: "top 50%",
          scrub: 1, // Smoothly animate while scrolling
          // markers: true, // Uncomment for debugging ScrollTrigger
        }
      }
    );

    // Example 3: About Section - A staggered animation for the text and image
    gsap.timeline({
      scrollTrigger: {
        trigger: "#about",
        start: "top 70%",
        end: "top 40%",
        scrub: 1,
        // markers: true,
      }
    })
    .fromTo("#about h2", { opacity: 0, x: -50 }, { opacity: 1, x: 0, duration: 1, ease: "power2.out" })
    .fromTo("#about p", { opacity: 0, x: -50 }, { opacity: 1, x: 0, duration: 1, ease: "power2.out" }, "-=0.5") // -0.5s delay after previous animation
    .fromTo("#about img", { opacity: 0, scale: 0.8 }, { opacity: 1, scale: 1, duration: 1, ease: "back.out(1.7)" }, "-=0.7");


    // Clean up function for Intersection Observer
    return () => {
      sectionIds.forEach(id => {
        const section = document.getElementById(id);
        if (section) observer.unobserve(section);
      });
      // Optionally kill GSAP tweens/ScrollTriggers on unmount
      // gsap.globalTimeline.clear();
      // ScrollTrigger.getAll().forEach(trigger => trigger.kill());
    };
  }, []); // Empty dependency array means this effect runs once on mount

  return (
    <motion.div
      initial={{ opacity: 0, y: 20 }}
      animate={{ opacity: 1, y: 0 }}
      exit={{ opacity: 0, y: -20 }}
      transition={{ duration: 0.5, ease: 'easeInOut' }}
      className="bg-[#070808] text-white font-inter w-full min-h-screen antialiased"
    >
      <Navbar activeSection={activeSection} setActiveSection={setActiveSection} />
      {/* Hero Section */}
      <section id="home" className="relative h-[1042px] w-full bg-cover bg-center" style={{ backgroundImage: "url('/section-1.png')" }}>
        <div className="absolute inset-0 bg-gradient-to-t from-[#070808] to-transparent"></div>
        <div className="relative z-10 flex flex-col h-full px-6 md:px-12 lg:px-[113px] pt-[150px]">
          <h1
            // data-aos="fade-right" // Removed AOS here to let GSAP control it
            // data-aos-delay="200"
            className="text-6xl md:text-7xl lg:text-[102px] leading-tight md:leading-[88.5px] font-libre-baskerville font-bold text-white mb-6 md:mb-12 max-w-2xl"
          >
            FitLogic: Beyond Limits
          </h1>
          <p
            // data-aos="fade-left" // Removed AOS here to let GSAP control it
            // data-aos-delay="400"
            className="font-courier-prime font-normal text-lg md:text-xl leading-[1.75] text-gray-300 max-w-lg"
          >
            Unlock your full potential with my cutting-edge fitness Algorithm. Experience the power of personalized Workout Plans tailored to your needs, and let my Calorie Calculator determine your essential caloric intake, precisely balancing carbs and protein for optimal results.
          </p>
        </div>
      </section>

      {/* Workout and Calorie Calculator Section */}
      <section id="workout" className="relative h-[1516px] w-full bg-[#070808] py-20 overflow-hidden">
        <div className="absolute inset-0 [background:url('/section-2.png')_50%_50%_/_cover] opacity-30"></div>

        <div className="relative z-10 flex flex-col items-end px-6 md:px-12 lg:px-24">
          <div className="w-full max-w-xl text-right mb-40 lg:mb-60" data-aos="fade-left"> {/* Kept AOS here */}
            <h2 className="text-5xl md:text-6xl leading-[1.2] font-libre-baskerville font-bold text-white mb-6">
              Elevate Your Fitness Journey
            </h2>
            <p className="font-courier-prime font-bold text-lg md:text-xl leading-[1.6] text-gray-400 mb-12">
              Effortlessly create your perfect workout. Our algorithm crafts personalized plans you can easily fine-tune to match your style and goals.
            </p>
            <Card
              onClick={() => router.push('/workoutplan')}
              className="w-full max-w-md ml-auto py-4 px-6 transition-transform duration-300 hover:scale-[1.02] cursor-pointer"
            >
              <CardContent className="flex items-center justify-center h-full p-0">
                <span className="font-courier-prime font-bold text-2xl text-purple-400">
                  Workout Planner
                </span>
              </CardContent>
            </Card>
          </div>
        </div>
        <div className="relative z-10 flex flex-col items-start px-6 md:px-12 lg:px-24">
          <div
            id="calorie-calc"
            className="w-full max-w-xl text-left mt-80 md:mt-96 lg:mt-[300px]"
            data-aos="fade-right" // Kept AOS here
            data-aos-delay="200"
          >
            <h2 className="text-5xl md:text-6xl leading-[1.2] font-libre-baskerville font-bold text-white mb-6">
              Calories
              <br />
              Calculator
            </h2>
            <p className="font-courier-prime font-bold text-lg md:text-xl leading-[1.6] text-gray-400 mb-12">
              Our calculator delivers a detailed breakdown of the calories your body requires.
            </p>
            <Card
              onClick={() => router.push('/CaloriesCalculator')}
              className="w-full max-w-md mx-auto lg:mx-0 py-4 px-6 transition-transform duration-300 hover:scale-[1.02] cursor-pointer"
            >
              <CardContent className="flex items-center justify-center h-full p-0">
                <span className="font-courier-prime font-bold text-2xl text-purple-400">
                  Calories Calculator
                </span>
              </CardContent>
            </Card>
          </div>
        </div>
      </section>

      {/* About Section */}
      <section id="about" className="h-auto w-full bg-[#070808] py-20">
        <div className="relative w-full max-w-[1333px] mx-auto px-6 md:px-12">
          <div className="flex flex-col lg:flex-row justify-between items-center lg:items-start lg:gap-x-20">
            <div className="w-full lg:w-1/2 flex flex-col text-center lg:text-left mb-16 lg:mb-0" data-aos="fade-right"> {/* Kept AOS here */}
              <h2 className="text-5xl md:text-6xl leading-[1.2] font-bold text-white font-libre-baskerville mb-6">
                About Us
              </h2>
              <p className="font-bold text-lg md:text-xl leading-[1.6] text-gray-400 font-courier-prime mb-10">
                Driven by a passion for fitness and tech, I built FitLogic to offer personalized, effective workout plans. Its core algorithm, developed through extensive research, makes fitness smarter and uniquely tailored to you.
              </p>

              <div className="mt-4 flex justify-center lg:justify-start">
                <ProfileCard
                  name="Mohamed Hossam"
                  title="Software Engineer"
                  handle="mohamedhossam123"
                  status="Online"
                  contactText="Email Me"
                  email="mohamedhossam25709@gmail.com"
                  avatarUrl="/default-avatar.jpg"
                  miniAvatarUrl="/default-avatar.jpg"
                  showUserInfo={true}
                  enableTilt={true}
                  linkedinUrl="https://www.linkedin.com/in/mohamed-hossam-070a1332a"
                  githubUrl="https://github.com/mohamedhossam123"
                  className="w-full max-w-sm rounded-xl border border-gray-800 bg-gradient-to-br from-gray-900 to-black shadow-2xl transition-transform duration-300 hover:scale-[1.01]"
                />
              </div>
            </div>

            <div className="w-full lg:w-1/2 flex-shrink-0 flex justify-center lg:justify-end items-center pl-4 lg:pl-12 xl:pl-24" data-aos="zoom-in-left" data-aos-delay="200"> {/* Kept AOS here */}
              <img
                className="w-full max-w-lg lg:max-w-none h-[500px] md:h-[600px] lg:h-[800px] object-cover rounded-xl shadow-lg"
                alt="About us image"
                src="/bottomimg1.png"
              />
            </div>
          </div>
        </div>
      </section>
    </motion.div>
  );
};