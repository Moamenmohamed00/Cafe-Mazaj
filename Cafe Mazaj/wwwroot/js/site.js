/* ══════════════════════════════════════════════
   site.js — GSAP Animations + Navbar Scroll
   Three.js canvas placeholder (Phase 3)
══════════════════════════════════════════════ */

document.addEventListener('DOMContentLoaded', function () {

  // ── Navbar scroll effect ───────────────────
  const navbar = document.getElementById('main-navbar');
  if (navbar) {
    window.addEventListener('scroll', function () {
      if (window.scrollY > 60) {
        navbar.classList.add('scrolled');
      } else {
        navbar.classList.remove('scrolled');
      }
    });
  }

  // ── GSAP Animations ────────────────────────
  if (typeof gsap !== 'undefined' && typeof ScrollTrigger !== 'undefined') {
    gsap.registerPlugin(ScrollTrigger);

    // Hero: staggered entrance
    const heroItems = document.querySelectorAll('.hero-content .gsap-fade-up');
    if (heroItems.length) {
      gsap.fromTo(heroItems,
        { opacity: 0, y: 40 },
        { opacity: 1, y: 0, duration: 1, stagger: 0.2, ease: 'power3.out', delay: 0.3 }
      );
    }

    // Fade-up on scroll
    document.querySelectorAll('.gsap-fade-up').forEach(el => {
      if (el.closest('.hero-content')) return; // already animated
      gsap.fromTo(el,
        { opacity: 0, y: 50 },
        {
          opacity: 1, y: 0, duration: 0.8, ease: 'power3.out',
          scrollTrigger: { trigger: el, start: 'top 85%', toggleActions: 'play none none none' }
        }
      );
    });

    // Slide right
    document.querySelectorAll('.gsap-slide-right').forEach(el => {
      gsap.fromTo(el,
        { opacity: 0, x: -60 },
        {
          opacity: 1, x: 0, duration: 0.9, ease: 'power3.out',
          scrollTrigger: { trigger: el, start: 'top 85%', toggleActions: 'play none none none' }
        }
      );
    });

    // Slide left
    document.querySelectorAll('.gsap-slide-left').forEach(el => {
      gsap.fromTo(el,
        { opacity: 0, x: 60 },
        {
          opacity: 1, x: 0, duration: 0.9, ease: 'power3.out',
          scrollTrigger: { trigger: el, start: 'top 85%', toggleActions: 'play none none none' }
        }
      );
    });

    // Staggered cards
    document.querySelectorAll('.product-grid, .testimonials-track').forEach(grid => {
      const cards = grid.querySelectorAll('.gsap-card');
      if (cards.length) {
        gsap.fromTo(cards,
          { opacity: 0, y: 40, scale: 0.95 },
          {
            opacity: 1, y: 0, scale: 1, duration: 0.6, stagger: 0.1, ease: 'power3.out',
            scrollTrigger: { trigger: grid, start: 'top 80%', toggleActions: 'play none none none' }
          }
        );
      }
    });

    // Animated counters
    document.querySelectorAll('.stat-number').forEach(el => {
      const target = parseInt(el.getAttribute('data-target') || '0', 10);
      gsap.fromTo({ val: 0 },
        { val: 0 },
        {
          val: target,
          duration: 2,
          ease: 'power2.out',
          scrollTrigger: { trigger: el, start: 'top 85%', once: true },
          onUpdate: function () {
            el.textContent = Math.floor(this.targets()[0].val).toLocaleString();
          }
        }
      );
    });
  }

  // ── Hero Canvas (placeholder — Three.js in Phase 3) ──
  const canvas = document.getElementById('hero-canvas');
  if (canvas) {
    // Animated gradient canvas fallback
    const ctx = canvas.getContext('2d');

    function resize() {
      canvas.width = window.innerWidth;
      canvas.height = window.innerHeight;
    }
    resize();
    window.addEventListener('resize', resize);

    // Floating particles
    const particles = Array.from({ length: 60 }, () => ({
      x: Math.random() * window.innerWidth,
      y: Math.random() * window.innerHeight,
      r: Math.random() * 2 + 0.5,
      vx: (Math.random() - 0.5) * 0.4,
      vy: -Math.random() * 0.5 - 0.1,
      alpha: Math.random() * 0.5 + 0.1
    }));

    function drawParticles() {
      ctx.clearRect(0, 0, canvas.width, canvas.height);

      // Dark bg gradient
      const grd = ctx.createRadialGradient(
        canvas.width / 2, canvas.height / 2, 0,
        canvas.width / 2, canvas.height / 2, canvas.width * 0.8
      );
      grd.addColorStop(0, '#1C1008');
      grd.addColorStop(1, '#0A0705');
      ctx.fillStyle = grd;
      ctx.fillRect(0, 0, canvas.width, canvas.height);

      particles.forEach(p => {
        ctx.beginPath();
        ctx.arc(p.x, p.y, p.r, 0, Math.PI * 2);
        ctx.fillStyle = `rgba(200,135,58,${p.alpha})`;
        ctx.fill();

        p.x += p.vx;
        p.y += p.vy;

        if (p.y < -5) { p.y = canvas.height + 5; p.x = Math.random() * canvas.width; }
        if (p.x < 0) p.x = canvas.width;
        if (p.x > canvas.width) p.x = 0;
      });

      requestAnimationFrame(drawParticles);
    }
    drawParticles();
  }

  // ── GLightbox ─────────────────────────────────────────
  if (typeof GLightbox !== 'undefined') {
    GLightbox({ selector: '.glightbox' });
  }

});
