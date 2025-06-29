'use client';

import React from 'react';
import { motion } from 'framer-motion';

const LoadingScreen: React.FC = () => {
  return (
    <motion.div
      initial={{ opacity: 0 }}
      animate={{ opacity: 1 }}
      exit={{ opacity: 0 }}
      transition={{ duration: 0.5, ease: 'easeInOut' }}
      className="fixed inset-0 bg-gradient-to-br from-zinc-900 via-black to-purple-900 flex flex-col items-center justify-center z-[9999] text-white overflow-hidden"
    >
      {/* Subtle grid pattern overlay for gym floor texture */}
      <div className="absolute inset-0 opacity-10 bg-gradient-to-b from-transparent via-zinc-700 to-transparent"></div>
      <div 
        className="absolute inset-0 opacity-5"
        style={{
          backgroundImage: `radial-gradient(circle at 2px 2px, rgba(255,255,255,0.15) 1px, transparent 0)`,
          backgroundSize: '40px 40px'
        }}
      ></div>

      {/* Animated logo with intense gym-style effects */}
      <motion.div
        initial={{ scale: 0.3, opacity: 0, rotateY: 180 }}
        animate={{ scale: 1, opacity: 1, rotateY: 0 }}
        transition={{ 
          type: "spring", 
          stiffness: 100, 
          damping: 15, 
          delay: 0.2,
          duration: 1.2
        }}
        className="relative mb-8"
      >
        {/* Glowing backdrop effect */}
        <motion.div
          animate={{ 
            boxShadow: [
              "0 0 20px rgba(126, 34, 206, 0.2)",
              "0 0 30px rgba(126, 34, 206, 0.3)",
              "0 0 20px rgba(126, 34, 206, 0.2)"
            ]
          }}
          transition={{ duration: 2, repeat: Infinity, ease: "easeInOut" }}
          className="absolute inset-0 rounded-full blur-xl"
        />
        
        <motion.img
          src="/logo-nav--removebg-preview.png"
          alt="FitLogic Logo"
          className="w-56 h-56 md:w-72 md:h-72 object-contain relative z-10 drop-shadow-2xl"
          animate={{ 
            filter: [
              "contrast(1.1) brightness(1)",
              "contrast(1.3) brightness(1.1)",
              "contrast(1.1) brightness(1)"
            ]
          }}
          transition={{ duration: 3, repeat: Infinity, ease: "easeInOut" }}
        />
      </motion.div>

      {/* Brand name with powerful typography */}
      <motion.div
        initial={{ y: 30, opacity: 0 }}
        animate={{ y: 0, opacity: 1 }}
        transition={{ delay: 0.6, duration: 0.8, ease: 'easeOut' }}
        className="text-center mb-4"
      >
        <motion.h1
          animate={{ 
            textShadow: [
              "0 0 10px rgba(147, 51, 234, 0.5)",
              "0 0 20px rgba(147, 51, 234, 0.8)",
              "0 0 10px rgba(147, 51, 234, 0.5)"
            ]
          }}
          transition={{ duration: 2.5, repeat: Infinity, ease: "easeInOut" }}
          className="text-5xl md:text-7xl font-black tracking-tighter text-white"
          style={{ 
            fontFamily: 'system-ui, -apple-system, sans-serif',
            letterSpacing: '-0.05em',
            textTransform: 'uppercase'
          }}
        >
          FitLogic
        </motion.h1>
      </motion.div>

      {/* Motivational tagline */}
      <motion.p
        initial={{ y: 20, opacity: 0 }}
        animate={{ y: 0, opacity: 1 }}
        transition={{ delay: 0.8, duration: 0.6, ease: 'easeOut' }}
        className="text-lg md:text-xl font-bold text-zinc-300 tracking-widest uppercase mb-8"
        style={{ fontFamily: 'monospace' }}
      >
        NO LIMITS. NO EXCUSES.
      </motion.p>

      {/* Intense loading indicator - barbell style */}
      <motion.div className="relative flex items-center">
        {/* Barbell bar */}
        <motion.div
          animate={{ 
            scaleX: [1, 1.1, 1],
            backgroundColor: [
              "rgb(147, 51, 234)",
              "rgb(126, 34, 206)", 
              "rgb(147, 51, 234)"
            ]
          }}
          transition={{ duration: 1.5, repeat: Infinity, ease: "easeInOut" }}
          className="w-32 h-2 bg-purple-600 rounded-full relative overflow-hidden"
        >
          {/* Moving highlight effect */}
          <motion.div
            animate={{ x: ["-100%", "200%"] }}
            transition={{ duration: 2, repeat: Infinity, ease: "linear" }}
            className="absolute top-0 left-0 w-8 h-full bg-gradient-to-r from-transparent via-white to-transparent opacity-30"
          />
        </motion.div>
        
        {/* Left weight plate */}
        <motion.div
          animate={{ 
            rotate: [0, 360],
            scale: [1, 1.05, 1]
          }}
          transition={{ 
            rotate: { duration: 3, repeat: Infinity, ease: "linear" },
            scale: { duration: 1.5, repeat: Infinity, ease: "easeInOut" }
          }}
          className="w-6 h-8 bg-zinc-600 rounded-sm -ml-1 border-2 border-zinc-500"
        />
        
        {/* Right weight plate */}
        <motion.div
          animate={{ 
            rotate: [0, -360],
            scale: [1, 1.05, 1]
          }}
          transition={{ 
            rotate: { duration: 3, repeat: Infinity, ease: "linear" },
            scale: { duration: 1.5, repeat: Infinity, ease: "easeInOut" }
          }}
          className="w-6 h-8 bg-zinc-600 rounded-sm -mr-1 border-2 border-zinc-500"
        />
      </motion.div>

      {/* Progress indicator */}
      <motion.div
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        transition={{ delay: 1, duration: 0.5 }}
        className="mt-6"
      >
        <motion.span
          animate={{ opacity: [0.5, 1, 0.5] }}
          transition={{ duration: 2, repeat: Infinity, ease: "easeInOut" }}
          className="text-sm font-bold text-zinc-400 tracking-wider uppercase"
          style={{ fontFamily: 'monospace' }}
        >
          LOADING INTENSITY...
        </motion.span>
      </motion.div>

      {/* Subtle corner accent elements */}
      <motion.div
        animate={{ 
          opacity: [0.1, 0.3, 0.1],
          scale: [1, 1.05, 1]
        }}
        transition={{ duration: 4, repeat: Infinity, ease: "easeInOut" }}
        className="absolute top-8 left-8 w-16 h-16 border-l-4 border-t-4 border-purple-500 opacity-20"
      />
      <motion.div
        animate={{ 
          opacity: [0.1, 0.3, 0.1],
          scale: [1, 1.05, 1]
        }}
        transition={{ duration: 4, repeat: Infinity, ease: "easeInOut", delay: 2 }}
        className="absolute bottom-8 right-8 w-16 h-16 border-r-4 border-b-4 border-purple-500 opacity-20"
      />
    </motion.div>
  );
};

export default LoadingScreen;