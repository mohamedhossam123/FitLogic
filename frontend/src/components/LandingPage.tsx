'use client';

import React, { useEffect, useState, useCallback, useRef } from 'react';
import { motion } from 'framer-motion';
import AOS from 'aos';
import 'aos/dist/aos.css'; // This import is CRUCIAL for AOS styles
import ProfileCard from './ProfileCard'; // Ensure this path is correct for your project

// Reusable Card components (refined for better UI)
const Card: React.FC<React.HTMLAttributes<HTMLDivElement>> = ({ className, children, ...props }) => (
  <div className={`rounded-xl border border-gray-800 bg-gradient-to-br from-gray-900 to-black text-white shadow-2xl overflow-hidden ${className}`} {...props}>
    {children}
  </div>
);

const CardContent: React.FC<React.HTMLAttributes<HTMLDivElement>> = ({ className, children, ...props }) => (
  <div className={`p-8 ${className}`} {...props}> {/* Increased padding */}
    {children}
  </div>
);

export const LandingPage = (): JSX.Element => {
  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);
  const navRef = useRef<HTMLElement>(null);
  const [activeSection, setActiveSection] = useState('home'); // State for active link highlighting

  useEffect(() => {
    // Initialize AOS
    AOS.init({
      duration: 1000, // Slightly longer duration for smoother entry
      once: true, // Whether animation should happen only once - default
      easing: 'ease-out-cubic', // More natural easing (ensure this is defined in tailwind.config.js if you want to use custom easing classes)
      // Optional: Add an offset if elements are appearing too early or too late
      // offset: 120, // offset (in px) from the original trigger point
      // delay: 0, // values from 0 to 3000, with step 50ms
      // disable: 'mobile', // disable AOS on mobile devices
    });

    // Optional: Refresh AOS if content changes dynamically after initial load
    // This can be useful if your content changes after initial render and you want new elements to animate.
    // AOS.refresh(); 

    // Intersection Observer for active link highlighting
    const observer = new IntersectionObserver(
      (entries) => {
        entries.forEach((entry) => {
          // Check if the entry is intersecting AND its ID is one of our nav items
          // This prevents highlighting for other random elements that might intersect
          const navItemIds = navItems.map(item => item.href.substring(1));
          if (entry.isIntersecting && navItemIds.includes(entry.target.id)) {
            setActiveSection(entry.target.id);
          }
        });
      },
      {
        root: null,
        rootMargin: '-50% 0px -50% 0px', // Trigger when section is roughly in the middle of the viewport
        threshold: 0,
      }
    );

    // Observe all sections linked in navItems
    navItems.forEach(item => {
      const section = document.getElementById(item.href.substring(1));
      if (section) observer.observe(section);
    });

    // Cleanup function for Intersection Observer
    return () => {
      // It's good practice to unobserve all elements when the component unmounts
      navItems.forEach(item => {
        const section = document.getElementById(item.href.substring(1));
        if (section) observer.unobserve(section);
      });
      // Optionally, if you had multiple AOS initializations or issues, you might use:
      // AOS.refreshHard(); // Recalculates all offsets and positions
    };
  }, []); // Empty dependency array means this runs once on mount

  const navItems = [
    { label: 'Home', href: '#home' },
    { label: 'Workout', href: '#workout' },
    { label: 'Calorie Calc', href: '#calorie-calc' },
    { label: 'About', href: '#about' },
  ];

  const handleProfileContactClick = useCallback(() => {
    // This will open the user's default email client
    window.location.href = `mailto:mohamedhossam25709@gmail.com`;
  }, []);

  const toggleMobileMenu = () => {
    setIsMobileMenuOpen(!isMobileMenuOpen);
  };

  const handleClickOutside = useCallback((event: MouseEvent) => {
    // Close mobile menu if click is outside the nav and it's open
    if (navRef.current && !navRef.current.contains(event.target as Node) && isMobileMenuOpen) {
      setIsMobileMenuOpen(false);
    }
  }, [isMobileMenuOpen]); // Dependency added for safety

  useEffect(() => {
    if (isMobileMenuOpen) {
      document.addEventListener('mousedown', handleClickOutside);
      document.body.style.overflow = 'hidden'; // Prevent scrolling when mobile menu is open
    } else {
      document.removeEventListener('mousedown', handleClickOutside);
      document.body.style.overflow = 'unset'; // Restore scrolling
    }
    // Cleanup event listener on unmount or when isMobileMenuOpen changes
    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
      document.body.style.overflow = 'unset';
    };
  }, [isMobileMenuOpen, handleClickOutside]);

  const handleNavLinkClick = (e: React.MouseEvent<HTMLAnchorElement>, href: string) => {
    e.preventDefault(); // Prevent default anchor link behavior
    const targetId = href.substring(1); // Get the ID from the href (e.g., "home", "workout")
    const targetElement = document.getElementById(targetId);

    if (targetElement) {
      // Calculate scroll position considering the fixed header
      const headerOffset = 70; // Height of your fixed header in pixels
      // getBoundingClientRect().top gives position relative to viewport
      // window.scrollY gives current scroll position from top of document
      const elementPosition = targetElement.getBoundingClientRect().top + window.scrollY;
      const offsetPosition = elementPosition - headerOffset;

      window.scrollTo({
        top: offsetPosition,
        behavior: 'smooth', // Smooth scroll animation
      });
      setIsMobileMenuOpen(false); // Close mobile menu after clicking a link
    }
  };

  return (
    <motion.div
      initial={{ opacity: 0, y: 20 }}
      animate={{ opacity: 1, y: 0 }}
      exit={{ opacity: 0, y: -20 }}
      transition={{ duration: 0.5, ease: 'easeInOut' }}
      className="bg-[#070808] text-white font-inter w-full min-h-screen antialiased" // Moved background and font styles here
    >
      {/* Navbar */}
      <header ref={navRef} className="flex w-full h-[70px] items-center justify-between px-4 md:px-10 lg:px-[100px] py-0 fixed top-0 left-0 z-50 backdrop-blur-md bg-black/60 shadow-lg">
        <div className="flex items-center gap-2">
          <img className="w-[45px] h-[45px] object-contain" alt="FitLogic Logo" src="/logo-nav--removebg-preview.png" /> {/* Adjusted logo size */}
          <span className="font-dmsans font-extrabold text-white text-3xl tracking-tight"> {/* Increased size, tracking */}
            FitLogic
          </span>
        </div>

        {/* Mobile Menu Button (Hamburger) */}
        <button
          className="lg:hidden text-white focus:outline-none focus:ring-2 focus:ring-purple-400 rounded p-1"
          onClick={toggleMobileMenu}
          aria-label="Toggle navigation menu"
        >
          <svg
            className="w-8 h-8 transition-transform duration-300 transform"
            fill="none"
            stroke="currentColor"
            viewBox="0 0 24 24"
            xmlns="http://www.w3.org/2000/svg"
          >
            {isMobileMenuOpen ? (
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="M6 18L18 6M6 6l12 12"
              ></path>
            ) : (
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="M4 6h16M4 12h16M4 18h16"
              ></path>
            )}
          </svg>
        </button>

        {/* Desktop Navigation */}
        <nav className="hidden lg:flex items-center gap-8"> {/* Increased gap */}
          {navItems.map((item, index) => (
            <a
              key={index}
              href={item.href}
              onClick={(e) => handleNavLinkClick(e, item.href)}
              className={`font-courier-prime font-medium text-lg leading-4 relative group
                          hover:text-purple-400 transition-colors duration-300
                          ${activeSection === item.href.substring(1) ? 'text-purple-400' : 'text-gray-400'}`}
            >
              {item.label}
              {/* Underline effect on hover/active */}
              <span className={`absolute bottom-0 left-0 w-full h-0.5 bg-purple-400 transform scale-x-0 group-hover:scale-x-100 transition-transform duration-300 origin-left
                               ${activeSection === item.href.substring(1) ? 'scale-x-100' : ''}`}></span>
            </a>
          ))}
        </nav>

        {/* Mobile Navigation (Conditional Rendering) */}
        {isMobileMenuOpen && (
          <div className="lg:hidden absolute top-[70px] left-0 w-full bg-black/90 flex flex-col items-center py-6 space-y-6 shadow-xl animate-fade-in-down">
            {navItems.map((item, index) => (
              <a
                key={index}
                href={item.href}
                onClick={(e) => handleNavLinkClick(e, item.href)}
                className={`font-courier-prime font-bold text-xl hover:text-purple-400 transition-colors w-full text-center py-3
                                ${activeSection === item.href.substring(1) ? 'text-purple-400 bg-gray-800' : 'text-gray-300'}`}
              >
                {item.label}
              </a>
            ))}
          </div>
        )}
      </header>

      {/* Hero Section */}
      <section id="home" className="relative h-[1042px] w-full bg-cover bg-center" style={{ backgroundImage: "url('/section-1.png')" }}>
        <div className="absolute inset-0 bg-gradient-to-t from-[#070808] to-transparent"></div> {/* Dark overlay for readability */}
        <div className="relative z-10 flex flex-col justify-center h-full px-6 md:px-12 lg:px-[113px] pt-0"> {/* Changed pt-12 to pt-0 */}
          <h1
            data-aos="fade-right"
            data-aos-delay="200"
            className="text-6xl md:text-7xl lg:text-[102px] leading-tight md:leading-[88.5px] font-libre-baskerville font-bold text-white mb-6 md:mb-12 max-w-2xl"
          >
            FitLogic: Beyond Limits
          </h1>
          <p
            data-aos="fade-left"
            data-aos-delay="400"
            className="font-courier-prime font-normal text-lg md:text-xl leading-[1.75] text-gray-300 max-w-lg"
          >
            Unlock your full potential with my cutting-edge fitness Algorithm. Experience the power of personalized Workout Plans tailored to your needs, and let my Calorie Calculator determine your essential caloric intake, precisely balancing carbs and protein for optimal results.
          </p>
        </div>
      </section>

      {/* Workout and Calorie Calculator Section (renamed for clarity, but ID is still 'workout') */}
      <section id="workout" className="relative h-[1516px] w-full bg-[#070808] py-20 overflow-hidden">
        <div className="absolute inset-0 [background:url('/section-2.png')_50%_50%_/_cover] opacity-30"></div> {/* Background with lower opacity */}
        <div className="relative z-10 flex flex-col items-end px-6 md:px-12 lg:px-24">
          <div
            className="w-full max-w-xl text-right mb-40 lg:mb-60"
            data-aos="fade-left"
          >
            <h2 className="text-5xl md:text-6xl leading-[1.2] font-libre-baskerville font-bold text-white mb-6">
              Elevate Your Fitness Journey
            </h2>
            <p className="font-courier-prime font-bold text-lg md:text-xl leading-[1.6] text-gray-400 mb-12">
              Effortlessly create your perfect workout. Our algorithm crafts personalized plans you can easily fine-tune to match your style and goals.
            </p>
            {/* ALIGNMENT FIX: Added ml-auto to right-align the card */}
            <Card className="w-full max-w-md ml-auto py-4 px-6 transition-transform duration-300 hover:scale-[1.02] cursor-pointer">
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
            id="calorie-calc" // This is the ID for the Calorie Calculator content
            className="w-full max-w-xl text-left mt-80 md:mt-96 lg:mt-[300px]" // Adjusted this line
            data-aos="fade-right"
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
            <Card className="w-full max-w-md mx-auto lg:mx-0 py-4 px-6 transition-transform duration-300 hover:scale-[1.02] cursor-pointer">
              <CardContent className="flex items-center justify-center h-full p-0">
                <span className="font-courier-prime font-bold text-2xl text-purple-400">
                  Calories Calculator
                </span>
              </CardContent>
            </Card>
          </div>
        </div>
      </section>

      {/* Footer / About Us Section */}
      <section id="about" className="h-auto w-full bg-[#070808] py-20">
        <div className="relative w-full max-w-[1333px] mx-auto px-6 md:px-12">
          <div className="flex flex-col lg:flex-row justify-between items-center lg:items-start lg:gap-x-20">
            {/* Left Column: About Us Text, Profile Card */}
            <div className="w-full lg:w-1/2 flex flex-col text-center lg:text-left mb-16 lg:mb-0" data-aos="fade-right">
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
    email="mohamedhossam25709@gmail.com" // ✅ pass email here
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

            {/* Right Column: Aesthetic Image (bottomimg.png) - MOVED MORE TO THE RIGHT */}
            <div className="w-full lg:w-1/2 flex-shrink-0 flex justify-center lg:justify-end items-center pl-4 lg:pl-12 xl:pl-24" data-aos="zoom-in-left" data-aos-delay="200">
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