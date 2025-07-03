'use client';

import React from 'react';
import { motion, Variants } from 'framer-motion';

const fadeIn: Variants = {
  initial: { opacity: 0 },
  animate: { opacity: 1, transition: { duration: 0.6, ease: 'easeInOut' } }, 
  exit: { opacity: 0, transition: { duration: 0.6, ease: 'easeInOut' } } 
};

const LoadingScreen: React.FC = () => {
  return (
    <motion.div
      {...fadeIn} // This now correctly spreads initial, animate, and exit states
      className="fixed inset-0 bg-gradient-to-br from-black via-zinc-950 to-purple-950 flex flex-col items-center justify-center z-[9999] text-white overflow-hidden"
    >
      {/* Gym floor texture overlays */}
      <div className="absolute inset-0 bg-[radial-gradient(circle_at_2px_2px,rgba(255,255,255,0.07)_1px,transparent_0)] opacity-5" style={{ backgroundSize: '40px 40px' }} />
      <div className="absolute inset-0 bg-gradient-to-b from-transparent via-zinc-800 to-transparent opacity-10" />

      {/* Glowing logo container */}
      <motion.div
        initial={{ scale: 0.3, opacity: 0, rotateY: 180 }}
        animate={{ scale: 1, opacity: 1, rotateY: 0 }}
        transition={{ type: 'spring', stiffness: 80, damping: 14, delay: 0.3 }}
        className="relative mb-10 rounded-full p-6 backdrop-blur-2xl bg-white/5 shadow-2xl shadow-purple-900"
      >
        {/* Glow ring */}
        <motion.div
          animate={{
            boxShadow: [
              '0 0 30px rgba(168,85,247,0.1)',
              '0 0 60px rgba(168,85,247,0.2)',
              '0 0 30px rgba(168,85,247,0.1)'
            ]
          }}
          transition={{ duration: 3, repeat: Infinity, ease: 'easeInOut' }}
          className="absolute inset-0 rounded-full blur-xl"
        />

        <motion.img
          src="/logo-nav--removebg-preview.png"
          alt="FitLogic Logo"
          className="w-56 h-56 md:w-72 md:h-72 object-contain relative z-10 drop-shadow-[0_10px_30px_rgba(168,85,247,0.5)]"
          animate={{
            filter: [
              'brightness(0.9) contrast(1.2)',
              'brightness(1.1) contrast(1.4)',
              'brightness(0.9) contrast(1.2)'
            ]
          }}
          transition={{ duration: 3.2, repeat: Infinity, ease: 'easeInOut' }}
        />
      </motion.div>

      {/* FitLogic Name */}
      <motion.h1
        initial={{ y: 40, opacity: 0 }}
        animate={{ y: 0, opacity: 1 }}
        transition={{ delay: 0.6, duration: 0.9, ease: 'easeOut' }}
        className="text-5xl md:text-7xl font-extrabold tracking-tight uppercase text-white text-center"
        style={{
          letterSpacing: '-0.03em',
          fontFamily: 'system-ui, sans-serif',
          textShadow: '0 0 20px rgba(168,85,247,0.4)'
        }}
      >
        FitLogic
      </motion.h1>

      {/* Tagline */}
      <motion.p
        initial={{ y: 20, opacity: 0 }}
        animate={{ y: 0, opacity: 1 }}
        transition={{ delay: 0.8, duration: 0.6, ease: 'easeOut' }}
        className="text-md md:text-xl font-semibold text-zinc-400 tracking-widest uppercase mb-10"
        style={{ fontFamily: 'monospace' }}
      >
        No Limits. No Excuses.
      </motion.p>

      {/* Premium barbell loader */}
      <motion.div className="relative flex items-center mb-6">
        {/* Bar */}
        <motion.div
          animate={{
            scaleX: [1, 1.05, 1],
            backgroundColor: [
              'rgba(168,85,247,0.6)',
              'rgba(139,92,246,0.8)',
              'rgba(168,85,247,0.6)'
            ]
          }}
          transition={{ duration: 1.8, repeat: Infinity, ease: 'easeInOut' }}
          className="w-36 h-2 rounded-full bg-purple-700 relative overflow-hidden"
        >
          {/* Glow streak */}
          <motion.div
            animate={{ x: ['-100%', '200%'] }}
            transition={{ duration: 2, repeat: Infinity, ease: 'linear' }}
            className="absolute top-0 left-0 w-8 h-full bg-gradient-to-r from-transparent via-white to-transparent opacity-30"
          />
        </motion.div>

        {/* Plates */}
        {[1, -1].map((dir, i) => (
          <motion.div
            key={i}
            animate={{
              rotate: dir * 360,
              scale: [1, 1.05, 1]
            }}
            transition={{
              rotate: { duration: 3, repeat: Infinity, ease: 'linear' },
              scale: { duration: 1.6, repeat: Infinity, ease: 'easeInOut' }
            }}
            className={`w-6 h-8 bg-zinc-700 border-2 border-zinc-600 rounded-sm ${dir > 0 ? '-ml-1' : '-mr-1'}`}
          />
        ))}
      </motion.div>

      {/* Subtle status text */}
      <motion.span
        animate={{ opacity: [0.5, 1, 0.5] }}
        transition={{ duration: 2.5, repeat: Infinity, ease: 'easeInOut' }}
        className="text-sm font-bold text-zinc-500 tracking-widest uppercase"
        style={{ fontFamily: 'monospace' }}
      >
        Loading intensity...
      </motion.span>

      <motion.div
        animate={{ opacity: [0.1, 0.3, 0.1], scale: [1, 1.05, 1] }}
        transition={{ duration: 5, repeat: Infinity, ease: 'easeInOut' }}
        className="absolute top-6 left-6 w-16 h-16 border-l-4 border-t-4 border-purple-800 opacity-20"
      />
      <motion.div
        animate={{ opacity: [0.1, 0.3, 0.1], scale: [1, 1.05, 1] }}
        transition={{ duration: 5, repeat: Infinity, ease: 'easeInOut', delay: 2.5 }}
        className="absolute bottom-6 right-6 w-16 h-16 border-r-4 border-b-4 border-purple-800 opacity-20"
      />
    </motion.div>
  );
};

export default LoadingScreen;