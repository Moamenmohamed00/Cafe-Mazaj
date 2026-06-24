document.addEventListener('DOMContentLoaded', function () {
  const reducedMotion = window.matchMedia('(prefers-reduced-motion: reduce)').matches;
  const easeOut = 'power3.out';
  const easeSpring = 'back.out(1.15)';

  initNavbar();
  initGsapAnimations();
  initHeroCanvas();
  initLightbox();

  function initNavbar() {
    const navbar = document.getElementById('main-navbar');
    if (!navbar) return;

    let ticking = false;

    function updateNavbarState() {
      navbar.classList.toggle('scrolled', window.scrollY > 28);
      ticking = false;
    }

    updateNavbarState();

    window.addEventListener('scroll', function () {
      if (ticking) return;

      ticking = true;
      window.requestAnimationFrame(updateNavbarState);
    }, { passive: true });
  }

  function initGsapAnimations() {
    if (typeof gsap === 'undefined' || typeof ScrollTrigger === 'undefined') {
      revealStaticContent();
      return;
    }

    gsap.registerPlugin(ScrollTrigger);
    gsap.defaults({ ease: easeOut });

    if (reducedMotion) {
      revealStaticContent();
      ScrollTrigger.refresh();
      return;
    }

    animateHero();
    animateContentBlocks();
    animateCards();
    animateCounters();
  }

  function animateHero() {
    const heroItems = document.querySelectorAll('.hero-content .gsap-fade-up');
    if (!heroItems.length) return;

    gsap.fromTo(heroItems,
      { autoAlpha: 0, y: 34, filter: 'blur(10px)' },
      {
        autoAlpha: 1,
        y: 0,
        filter: 'blur(0px)',
        duration: 1.15,
        stagger: 0.16,
        delay: 0.18,
        ease: easeOut
      }
    );
  }

  function animateContentBlocks() {
    document.querySelectorAll('.gsap-fade-up').forEach(function (el) {
      if (el.closest('.hero-content')) return;

      gsap.fromTo(el,
        { autoAlpha: 0, y: 36, filter: 'blur(8px)' },
        {
          autoAlpha: 1,
          y: 0,
          filter: 'blur(0px)',
          duration: 0.9,
          scrollTrigger: {
            trigger: el,
            start: 'top 86%',
            once: true
          }
        }
      );
    });

    document.querySelectorAll('.gsap-slide-right, .gsap-slide-left').forEach(function (el) {
      const direction = el.classList.contains('gsap-slide-right') ? -1 : 1;

      gsap.fromTo(el,
        { autoAlpha: 0, x: 46 * direction, filter: 'blur(8px)' },
        {
          autoAlpha: 1,
          x: 0,
          filter: 'blur(0px)',
          duration: 1,
          scrollTrigger: {
            trigger: el,
            start: 'top 84%',
            once: true
          }
        }
      );
    });
  }

  function animateCards() {
    document.querySelectorAll('.product-grid, .testimonials-track, #menuGrid').forEach(function (grid) {
      const cards = grid.querySelectorAll('.gsap-card, .menu-item, .testimonial-card');
      if (!cards.length) return;

      gsap.fromTo(cards,
        { autoAlpha: 0, y: 34, scale: 0.965, filter: 'blur(8px)' },
        {
          autoAlpha: 1,
          y: 0,
          scale: 1,
          filter: 'blur(0px)',
          duration: 0.82,
          stagger: 0.09,
          ease: easeSpring,
          scrollTrigger: {
            trigger: grid,
            start: 'top 82%',
            once: true
          }
        }
      );
    });
  }

  function animateCounters() {
    document.querySelectorAll('.stat-number').forEach(function (el) {
      const target = parseInt(el.getAttribute('data-target') || '0', 10);

      gsap.fromTo({ value: 0 },
        { value: 0 },
        {
          value: target,
          duration: 2.1,
          ease: 'power2.out',
          scrollTrigger: {
            trigger: el,
            start: 'top 86%',
            once: true
          },
          onUpdate: function () {
            el.textContent = Math.floor(this.targets()[0].value).toLocaleString();
          }
        }
      );
    });
  }

  function revealStaticContent() {
    document
      .querySelectorAll('.gsap-fade-up, .gsap-slide-right, .gsap-slide-left, .gsap-card')
      .forEach(function (el) {
        el.style.opacity = '1';
        el.style.visibility = 'visible';
        el.style.transform = 'none';
        el.style.filter = 'none';
      });
  }

  function initHeroCanvas() {
    const canvas = document.getElementById('hero-canvas');
    if (!canvas) return;

    const ctx = canvas.getContext('2d');
    if (!ctx) return;

    let width = 0;
    let height = 0;
    let rafId = 0;
    let particles = [];
    const particleCount = reducedMotion ? 0 : Math.min(90, Math.max(42, Math.floor(window.innerWidth / 22)));

    function resize() {
      const dpr = Math.min(window.devicePixelRatio || 1, 2);
      width = window.innerWidth;
      height = window.innerHeight;
      canvas.width = Math.floor(width * dpr);
      canvas.height = Math.floor(height * dpr);
      canvas.style.width = width + 'px';
      canvas.style.height = height + 'px';
      ctx.setTransform(dpr, 0, 0, dpr, 0, 0);
      particles = createParticles(particleCount);
      drawFrame();
    }

    function createParticles(count) {
      return Array.from({ length: count }, function () {
        return {
          x: Math.random() * width,
          y: Math.random() * height,
          radius: Math.random() * 1.7 + 0.45,
          vx: (Math.random() - 0.5) * 0.18,
          vy: -Math.random() * 0.28 - 0.04,
          alpha: Math.random() * 0.28 + 0.08,
          drift: Math.random() * Math.PI * 2
        };
      });
    }

    function paintBackground() {
      const base = ctx.createRadialGradient(width * 0.5, height * 0.36, 0, width * 0.5, height * 0.5, width * 0.86);
      base.addColorStop(0, '#20130A');
      base.addColorStop(0.48, '#120B07');
      base.addColorStop(1, '#090604');
      ctx.fillStyle = base;
      ctx.fillRect(0, 0, width, height);

      paintGlow(width * 0.74, height * 0.28, Math.min(width, height) * 0.52, 'rgba(200, 135, 58, 0.12)');
      paintGlow(width * 0.18, height * 0.78, Math.min(width, height) * 0.44, 'rgba(232, 201, 154, 0.075)');
    }

    function paintGlow(x, y, radius, color) {
      const glow = ctx.createRadialGradient(x, y, 0, x, y, radius);
      glow.addColorStop(0, color);
      glow.addColorStop(1, 'rgba(200, 135, 58, 0)');
      ctx.fillStyle = glow;
      ctx.fillRect(x - radius, y - radius, radius * 2, radius * 2);
    }

    function drawParticles() {
      particles.forEach(function (particle) {
        particle.drift += 0.006;
        particle.x += particle.vx + Math.sin(particle.drift) * 0.05;
        particle.y += particle.vy;

        if (particle.y < -8) {
          particle.y = height + 8;
          particle.x = Math.random() * width;
        }

        if (particle.x < -8) particle.x = width + 8;
        if (particle.x > width + 8) particle.x = -8;

        ctx.beginPath();
        ctx.arc(particle.x, particle.y, particle.radius, 0, Math.PI * 2);
        ctx.fillStyle = 'rgba(232, 201, 154, ' + particle.alpha + ')';
        ctx.fill();
      });
    }

    function drawFrame() {
      ctx.clearRect(0, 0, width, height);
      paintBackground();
      drawParticles();
    }

    function tick() {
      drawFrame();
      rafId = window.requestAnimationFrame(tick);
    }

    resize();
    window.addEventListener('resize', debounce(resize, 160), { passive: true });

    if (!reducedMotion) {
      tick();
    }
  }

  function initLightbox() {
    if (typeof GLightbox !== 'undefined') {
      GLightbox({ selector: '.glightbox' });
    }
  }

  function debounce(fn, delay) {
    let timeoutId;

    return function () {
      window.clearTimeout(timeoutId);
      timeoutId = window.setTimeout(fn, delay);
    };
  }
});
