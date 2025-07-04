'use client';

import React, { useEffect, useRef } from 'react';
import { motion, Variants } from 'framer-motion';
import { gsap } from 'gsap'; // Import GSAP
import { CustomEase } from 'gsap/CustomEase'; // For custom easing (optional, but "all out"!)

gsap.registerPlugin(CustomEase); // Register CustomEase if you're using it

// Define Framer Motion variants for the overall fade in/out
// Corrected Framer Motion Variants
const fadeIn: Variants = {
  initial: { opacity: 0 },
  animate: { opacity: 1, transition: { duration: 0.8, ease: 'easeOut' } }, // Use Framer Motion's easeOut
  exit: { opacity: 0, transition: { duration: 0.6, ease: 'easeInOut' } } // Use Framer Motion's easeInOut
};
const LoadingScreen: React.FC = () => {
  // Refs for GSAP targets
  const logoContainerRef = useRef<HTMLDivElement>(null);
  const logoRingRef = useRef<HTMLDivElement>(null);
  const logoBgRef = useRef<HTMLDivElement>(null);
  const logoImgRef = useRef<HTMLImageElement>(null);
  const brandNameRef = useRef<HTMLHeadingElement>(null);
  const taglineRef = useRef<HTMLParagraphElement>(null);
  const progressBarRef = useRef<HTMLDivElement>(null);
  const progressDotRef = useRef<HTMLDivElement>(null);
  const loadingTextRef = useRef<HTMLSpanElement>(null);
  const loadingSpinnerRef = useRef<HTMLDivElement>(null);
  const cornerTopLeftRef = useRef<HTMLDivElement>(null);
  const cornerBottomRightRef = useRef<HTMLDivElement>(null);
  const particleRefs = useRef<HTMLDivElement[]>([]); // Array for particles

  useEffect(() => {
    // Ensure GSAP runs only on client side and elements are mounted
    if (
      !logoContainerRef.current ||
      !logoRingRef.current ||
      !logoBgRef.current ||
      !logoImgRef.current ||
      !brandNameRef.current ||
      !taglineRef.current ||
      !progressBarRef.current ||
      !progressDotRef.current ||
      !loadingTextRef.current ||
      !loadingSpinnerRef.current ||
      !cornerTopLeftRef.current ||
      !cornerBottomRightRef.current
    ) {
      return;
    }

    // Create a master timeline for orchestrating all animations
    const masterTimeline = gsap.timeline({ defaults: { ease: 'power3.out' } });

    // GSAP CustomEase (optional):
    // CustomEase.create("myEase", "M0,0 C0.16,1 0.3,1 1,1"); // Example custom ease

    // 1. Initial Logo Container scale and fade (replaces Framer Motion initial/animate for this)
    masterTimeline.fromTo(
      logoContainerRef.current,
      { scale: 0.8, opacity: 0, y: 30 },
      { scale: 1, opacity: 1, y: 0, duration: 1.2, ease: 'back.out(1.7)' },
      'start' // Label for sequencing
    );

    // 2. Elegant Ring rotation and subtle scale pulse (GSAP for more control)
    masterTimeline.to(
      logoRingRef.current,
      {
        rotate: 360,
        repeat: -1, // Infinite repeat
        ease: 'none', // Linear rotation
        duration: 20,
      },
      'start' // Starts with logo container animation
    );
    masterTimeline.to(
      logoRingRef.current,
      {
        scale: 1.02,
        yoyo: true, // Go back and forth
        repeat: -1,
        duration: 2, // Faster pulse
        ease: 'power1.inOut'
      },
      'start' // Starts with logo container animation
    );

    // 3. Logo background fade in
    masterTimeline.fromTo(
      logoBgRef.current,
      { opacity: 0 },
      { opacity: 1, duration: 1.2, ease: 'power2.out' },
      'start+=0.3' // Slightly delayed after logo container
    );

    // 4. Logo image scale and fade
    masterTimeline.fromTo(
      logoImgRef.current,
      { scale: 0.5, opacity: 0 },
      { scale: 1, opacity: 1, duration: 1, ease: 'elastic.out(1, 0.5)' }, // Bouncier entrance
      'start+=0.5' // Delayed after logo background
    );

    // 5. Subtle glow animation (GSAP with custom properties)
    gsap.to(logoBgRef.current, {
      boxShadow: '0 0 80px rgba(255,255,255,0.15)',
      repeat: -1,
      yoyo: true,
      duration: 3,
      ease: 'power1.inOut'
    });

    // 6. Brand name animation with a cool text effect
    masterTimeline.fromTo(
      brandNameRef.current,
      { y: 50, opacity: 0, clipPath: 'inset(100% 0 0 0)' }, // Clip path for reveal effect
      { y: 0, opacity: 1, clipPath: 'inset(0% 0 0 0)', duration: 1.2, ease: 'power4.out' },
      'start+=0.8' // Delayed
    );

    // 7. Elegant tagline
    masterTimeline.fromTo(
      taglineRef.current,
      { opacity: 0, y: 20 },
      { opacity: 1, y: 0, duration: 0.8, ease: 'power2.out' },
      'start+=1.2' // Delayed after brand name
    );

    // 8. Minimal progress indicator bar
    masterTimeline.fromTo(
      progressBarRef.current,
      { width: 0, opacity: 0 },
      { width: '300px', opacity: 1, duration: 1, ease: 'power2.out' },
      'start+=1.5' // Delayed
    );

    // 9. Progress dot animation along the bar
    masterTimeline.fromTo(
      progressDotRef.current,
      { x: '0%' }, // Use x for horizontal movement
      { x: 'calc(100% - 10px)', duration: 2.5, ease: 'power1.inOut', repeat: -1, yoyo: true }, // -10px to keep it within bounds
      'start+=1.8' // Delayed after progress bar appears
    );

    // 10. Loading text and spinner
    masterTimeline.fromTo(
      [loadingTextRef.current, loadingSpinnerRef.current],
      { opacity: 0, y: 10 },
      { opacity: 1, y: 0, duration: 0.7, ease: 'power2.out' },
      'start+=1.7' // Delayed
    );

    // Spinner continuous rotation (GSAP for precise control)
    gsap.to(loadingSpinnerRef.current, {
      rotate: 360,
      duration: 1.5,
      repeat: -1,
      ease: 'linear'
    });

    // Loading text pulse (GSAP for clean pulse)
    gsap.to(loadingTextRef.current, {
      opacity: 0.6,
      repeat: -1,
      yoyo: true,
      duration: 1.5,
      ease: 'sine.inOut'
    });

    // 11. Elegant corner decorations
    masterTimeline.fromTo(
      cornerTopLeftRef.current,
      { opacity: 0, scale: 0.8, x: -20, y: -20 },
      { opacity: 0.3, scale: 1, x: 0, y: 0, duration: 1.5, ease: 'power2.out' },
      'start+=1.2' // Delayed
    );
    masterTimeline.fromTo(
      cornerBottomRightRef.current,
      { opacity: 0, scale: 0.8, x: 20, y: 20 },
      { opacity: 0.3, scale: 1, x: 0, y: 0, duration: 1.5, ease: 'power2.out' },
      'start+=1.4' // Delayed
    );

    // 12. Floating particles (GSAP for more dynamic movement)
    // We'll need to create the particles in the JSX and then animate them.
    particleRefs.current.forEach((particle, i) => {
      if (particle) {
        gsap.fromTo(
          particle,
          {
            x: Math.random() * (window.innerWidth - 100) + 50, // More controlled x spread
            y: window.innerHeight + 50, // Start below view
            opacity: 0,
            scale: 0.5 + Math.random() * 0.5 // Vary size
          },
          {
            y: -50, // End above view
            opacity: 0.5,
            scale: 1,
            duration: 6 + Math.random() * 4, // Longer duration for slower float
            ease: 'none',
            repeat: -1,
            delay: Math.random() * 4, // Stagger delays
            onRepeat: () => {
              // Reset x position on repeat for continuous flow
              gsap.set(particle, { x: Math.random() * (window.innerWidth - 100) + 50 });
            }
          }
        );
      }
    });

    // Cleanup GSAP animations on component unmount
    return () => {
      masterTimeline.kill();
      gsap.killTweensOf(logoRingRef.current);
      gsap.killTweensOf(logoBgRef.current);
      gsap.killTweensOf(loadingSpinnerRef.current);
      gsap.killTweensOf(loadingTextRef.current);
      particleRefs.current.forEach(particle => {
        gsap.killTweensOf(particle);
      });
    };
  }, []); // Empty dependency array means this runs once on mount

  return (
    <motion.div
      {...fadeIn}
      className="fixed inset-0 bg-gradient-to-br from-gray-900 via-black to-gray-800 flex flex-col items-center justify-center z-[9999] text-white overflow-hidden"
    >
      {/* Subtle dark pattern */}
      <div className="absolute inset-0 opacity-10">
        <div className="w-full h-full bg-gradient-to-br from-white/5 via-transparent to-white/5" />
      </div>

      {/* Premium logo container */}
      <div ref={logoContainerRef} className="relative mb-12">
        {/* Elegant ring */}
        <div
          ref={logoRingRef}
          className="absolute -inset-8 rounded-2xl border border-white/10"
        />

        {/* Logo background */}
        <div
          ref={logoBgRef}
          className="relative w-72 h-72 flex items-center justify-center bg-gradient-to-br from-gray-800/30 to-gray-900/50 backdrop-blur-2xl rounded-2xl shadow-2xl shadow-black/60"
        >
          {/* Logo */}
          <img
            ref={logoImgRef}
            src="/logo-nav--removebg-preview.png"
            alt="FitLogic Logo"
            className="w-48 h-48 object-contain"
          />
        </div>
      </div>

      {/* Brand name */}
      <h1
        ref={brandNameRef}
        className="text-6xl md:text-7xl font-light tracking-wide mb-6 text-white"
        style={{
          fontFamily: '"Playfair Display", serif',
          letterSpacing: '0.05em'
        }}
      >
        FitLogic
      </h1>

      {/* Elegant tagline */}
      <p
        ref={taglineRef}
        className="text-lg font-light text-gray-300 tracking-widest uppercase mb-16"
        style={{ fontFamily: '"Inter", sans-serif' }}
      >
        Push Beyond Limits
      </p>

      {/* Minimal progress indicator */}
      <div
        ref={progressBarRef}
        className="relative h-px bg-gradient-to-r from-transparent via-white/30 to-transparent mb-8"
      >
        {/* Progress dot */}
        <div
          ref={progressDotRef}
          className="absolute top-1/2 -translate-y-1/2 w-2 h-2 bg-white rounded-full shadow-lg shadow-white/50"
        />
      </div>

      {/* Loading text */}
      <div className="flex items-center gap-3">
        <div
          ref={loadingSpinnerRef}
          className="w-5 h-5 border border-white/40 border-t-white rounded-full"
        />
        <span
          ref={loadingTextRef}
          className="text-sm font-light text-gray-300 tracking-widest"
          style={{ fontFamily: '"Inter", sans-serif' }}
        >
          Loading
        </span>
      </div>

      {/* Elegant corner decorations */}
      <div
        ref={cornerTopLeftRef}
        className="absolute top-10 left-10 w-8 h-8 border-l border-t border-white/20"
      />
      <div
        ref={cornerBottomRightRef}
        className="absolute bottom-10 right-10 w-8 h-8 border-r border-b border-white/20"
      />

      {/* Floating particles */}
      {[...Array(10)].map((_, i) => ( // Increased particles for more visual interest
        <div
          key={i}
          ref={el => { if (el) particleRefs.current[i] = el; }} // Assign ref
          className="absolute w-1 h-1 bg-white/20 rounded-full"
        />
      ))}

      {/* Ambient light */}
      <div className="absolute inset-0 bg-gradient-radial from-white/5 via-transparent to-transparent pointer-events-none" />
    </motion.div>
  );
};

export default LoadingScreen;