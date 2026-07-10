const COLS = 64;
const ROWS = 42;
const ROUND_SECONDS = 90;
const MIN_WIN_SECONDS = 10;
const PROGRESS_KEY = "netaJiCampaignProgressV1";
const ONBOARDING_KEY = "netaJiOnboardingSeenV1";
const MAP_ASPECT = 0.82;
const DEMO_REGION_ID = "uttar-pradesh";
const DEMO_PARTY = {
  name: "Momo Lovers Party",
  slogan: "Vote for extra chutney",
  color: "#f05d23",
  symbol: "star"
};
const DEMO_FROM_QUERY = new URLSearchParams(window.location.search).get("demo") === "1";
const MAP_QA_FROM_QUERY = new URLSearchParams(window.location.search).get("mapqa") === "1";
const ONBOARDING_STEPS = [
  {
    title: "Choose a state",
    copy: "Tap a black flag on the India map, then OK to open a big state arena."
  },
  {
    title: "Run the yatra",
    copy: "Swipe anywhere inside the arena. Your route leaves a dark campaign trail."
  },
  {
    title: "Close loops",
    copy: "Return to your color to win booths, trigger meme events, and export a result poster."
  }
];

const REGIONS = [
  { id: "andhra-pradesh", name: "Andhra Pradesh", type: "State" },
  { id: "arunachal-pradesh", name: "Arunachal Pradesh", type: "State" },
  { id: "assam", name: "Assam", type: "State" },
  { id: "bihar", name: "Bihar", type: "State" },
  { id: "chhattisgarh", name: "Chhattisgarh", type: "State" },
  { id: "goa", name: "Goa", type: "State" },
  { id: "gujarat", name: "Gujarat", type: "State" },
  { id: "haryana", name: "Haryana", type: "State" },
  { id: "himachal-pradesh", name: "Himachal Pradesh", type: "State" },
  { id: "jharkhand", name: "Jharkhand", type: "State" },
  { id: "karnataka", name: "Karnataka", type: "State" },
  { id: "kerala", name: "Kerala", type: "State" },
  { id: "madhya-pradesh", name: "Madhya Pradesh", type: "State" },
  { id: "maharashtra", name: "Maharashtra", type: "State" },
  { id: "manipur", name: "Manipur", type: "State" },
  { id: "meghalaya", name: "Meghalaya", type: "State" },
  { id: "mizoram", name: "Mizoram", type: "State" },
  { id: "nagaland", name: "Nagaland", type: "State" },
  { id: "odisha", name: "Odisha", type: "State" },
  { id: "punjab", name: "Punjab", type: "State" },
  { id: "rajasthan", name: "Rajasthan", type: "State" },
  { id: "sikkim", name: "Sikkim", type: "State" },
  { id: "tamil-nadu", name: "Tamil Nadu", type: "State" },
  { id: "telangana", name: "Telangana", type: "State" },
  { id: "tripura", name: "Tripura", type: "State" },
  { id: "uttar-pradesh", name: "Uttar Pradesh", type: "State" },
  { id: "uttarakhand", name: "Uttarakhand", type: "State" },
  { id: "west-bengal", name: "West Bengal", type: "State" },
  { id: "andaman-nicobar", name: "Andaman and Nicobar Islands", type: "UT" },
  { id: "chandigarh", name: "Chandigarh", type: "UT" },
  { id: "dadra-daman-diu", name: "Dadra and Nagar Haveli and Daman and Diu", type: "UT" },
  { id: "delhi", name: "Delhi", type: "UT" },
  { id: "jammu-kashmir", name: "Jammu and Kashmir", type: "UT" },
  { id: "ladakh", name: "Ladakh", type: "UT" },
  { id: "lakshadweep", name: "Lakshadweep", type: "UT" },
  { id: "puducherry", name: "Puducherry", type: "UT" }
];

const REGION_POSITIONS = {
  "andhra-pradesh": [0.58, 0.69],
  "arunachal-pradesh": [0.9, 0.27],
  assam: [0.82, 0.37],
  bihar: [0.64, 0.4],
  chhattisgarh: [0.53, 0.54],
  goa: [0.35, 0.73],
  gujarat: [0.21, 0.53],
  haryana: [0.4, 0.31],
  "himachal-pradesh": [0.43, 0.22],
  jharkhand: [0.62, 0.5],
  karnataka: [0.43, 0.74],
  kerala: [0.44, 0.89],
  "madhya-pradesh": [0.43, 0.48],
  maharashtra: [0.37, 0.61],
  manipur: [0.89, 0.45],
  meghalaya: [0.78, 0.42],
  mizoram: [0.87, 0.53],
  nagaland: [0.91, 0.39],
  odisha: [0.62, 0.59],
  punjab: [0.35, 0.25],
  rajasthan: [0.27, 0.39],
  sikkim: [0.7, 0.32],
  "tamil-nadu": [0.52, 0.85],
  telangana: [0.49, 0.62],
  tripura: [0.82, 0.51],
  "uttar-pradesh": [0.51, 0.38],
  uttarakhand: [0.49, 0.27],
  "west-bengal": [0.7, 0.49],
  "andaman-nicobar": [0.79, 0.88],
  chandigarh: [0.39, 0.25],
  "dadra-daman-diu": [0.28, 0.61],
  delhi: [0.43, 0.34],
  "jammu-kashmir": [0.35, 0.12],
  ladakh: [0.5, 0.1],
  lakshadweep: [0.24, 0.86],
  puducherry: [0.58, 0.83]
};

const INDIA_OUTLINE = [
  [0.41, 0.04],
  [0.34, 0.08],
  [0.29, 0.16],
  [0.34, 0.23],
  [0.28, 0.27],
  [0.25, 0.34],
  [0.18, 0.42],
  [0.21, 0.53],
  [0.27, 0.57],
  [0.29, 0.66],
  [0.35, 0.76],
  [0.42, 0.94],
  [0.5, 0.99],
  [0.56, 0.87],
  [0.57, 0.75],
  [0.65, 0.67],
  [0.68, 0.57],
  [0.75, 0.52],
  [0.72, 0.45],
  [0.66, 0.42],
  [0.64, 0.35],
  [0.57, 0.29],
  [0.55, 0.19],
  [0.48, 0.13],
  [0.47, 0.06]
];

const NORTH_EAST_OUTLINE = [
  [0.72, 0.34],
  [0.82, 0.27],
  [0.95, 0.27],
  [0.98, 0.36],
  [0.93, 0.47],
  [0.89, 0.57],
  [0.8, 0.52],
  [0.77, 0.45],
  [0.7, 0.43]
];

const REGION_BLOB_SIZE = {
  "jammu-kashmir": [0.09, 0.055],
  ladakh: [0.095, 0.05],
  rajasthan: [0.12, 0.085],
  gujarat: [0.1, 0.075],
  "madhya-pradesh": [0.12, 0.085],
  maharashtra: [0.12, 0.08],
  "uttar-pradesh": [0.12, 0.065],
  bihar: [0.09, 0.05],
  karnataka: [0.09, 0.085],
  "tamil-nadu": [0.085, 0.085],
  "west-bengal": [0.07, 0.08],
  "andaman-nicobar": [0.035, 0.08],
  lakshadweep: [0.03, 0.055],
  defaultState: [0.074, 0.056],
  defaultUt: [0.035, 0.035]
};

const REGION_SHAPE_TEMPLATES = {
  state: [
    [-0.2, -0.95],
    [0.58, -0.72],
    [0.98, -0.14],
    [0.72, 0.68],
    [0.08, 0.95],
    [-0.74, 0.54],
    [-0.98, -0.18],
    [-0.62, -0.72]
  ],
  compact: [
    [-0.12, -0.92],
    [0.72, -0.62],
    [0.9, 0.06],
    [0.42, 0.82],
    [-0.35, 0.86],
    [-0.88, 0.22],
    [-0.7, -0.52]
  ],
  wide: [
    [-0.95, -0.48],
    [-0.28, -0.76],
    [0.62, -0.58],
    [0.98, -0.08],
    [0.72, 0.5],
    [-0.06, 0.74],
    [-0.86, 0.44]
  ],
  vertical: [
    [-0.38, -0.92],
    [0.42, -0.78],
    [0.72, -0.22],
    [0.48, 0.5],
    [0.04, 0.98],
    [-0.58, 0.58],
    [-0.74, -0.14]
  ],
  rajasthan: [
    [-0.95, -0.58],
    [-0.22, -0.88],
    [0.8, -0.62],
    [0.98, 0.18],
    [0.36, 0.76],
    [-0.5, 0.72],
    [-0.98, 0.08]
  ],
  gujarat: [
    [-0.98, -0.34],
    [-0.3, -0.76],
    [0.58, -0.54],
    [0.94, 0.12],
    [0.46, 0.62],
    [-0.2, 0.82],
    [-0.52, 0.32],
    [-0.92, 0.54],
    [-0.7, 0.02]
  ],
  up: [
    [-0.98, -0.34],
    [-0.36, -0.7],
    [0.56, -0.56],
    [0.98, -0.06],
    [0.74, 0.48],
    [0.08, 0.68],
    [-0.62, 0.42]
  ],
  madhya: [
    [-0.88, -0.5],
    [-0.12, -0.78],
    [0.76, -0.52],
    [0.98, 0.08],
    [0.48, 0.7],
    [-0.24, 0.82],
    [-0.86, 0.38],
    [-0.98, -0.08]
  ],
  coastalWest: [
    [-0.58, -0.92],
    [0.32, -0.78],
    [0.76, -0.18],
    [0.52, 0.56],
    [0.06, 0.96],
    [-0.34, 0.62],
    [-0.72, -0.16]
  ],
  coastalEast: [
    [-0.44, -0.9],
    [0.54, -0.62],
    [0.86, -0.04],
    [0.52, 0.78],
    [-0.2, 0.92],
    [-0.72, 0.36],
    [-0.82, -0.36]
  ],
  kerala: [
    [-0.28, -0.96],
    [0.24, -0.8],
    [0.38, -0.18],
    [0.28, 0.58],
    [0.04, 0.98],
    [-0.34, 0.46],
    [-0.42, -0.34]
  ],
  tamil: [
    [-0.42, -0.82],
    [0.56, -0.66],
    [0.82, 0.06],
    [0.34, 0.96],
    [-0.32, 0.72],
    [-0.72, 0.1]
  ],
  bengal: [
    [-0.44, -0.92],
    [0.42, -0.72],
    [0.72, -0.12],
    [0.36, 0.26],
    [0.28, 0.92],
    [-0.22, 0.72],
    [-0.36, 0.1],
    [-0.72, -0.28]
  ],
  hill: [
    [-0.86, -0.24],
    [-0.42, -0.82],
    [0.44, -0.78],
    [0.9, -0.2],
    [0.58, 0.48],
    [-0.22, 0.76],
    [-0.84, 0.36]
  ],
  river: [
    [-0.98, -0.12],
    [-0.42, -0.48],
    [0.42, -0.38],
    [0.98, 0.02],
    [0.4, 0.44],
    [-0.38, 0.48],
    [-0.9, 0.18]
  ],
  island: [
    [-0.24, -0.98],
    [0.28, -0.58],
    [0.22, 0.4],
    [-0.06, 0.98],
    [-0.32, 0.2]
  ]
};

const REGION_SHAPE_PROFILES = {
  "andhra-pradesh": { template: "coastalEast", angle: 0.08, label: "AP" },
  "arunachal-pradesh": { template: "hill", scaleX: 1.35, scaleY: 0.8, label: "AR" },
  assam: { template: "river", scaleX: 1.25, scaleY: 0.78, label: "AS" },
  bihar: { template: "wide", scaleX: 1.1, scaleY: 0.72, label: "BR" },
  chhattisgarh: { template: "vertical", scaleX: 0.9, scaleY: 1.12, label: "CG" },
  goa: { template: "compact", scaleX: 0.82, scaleY: 0.82, label: "GA" },
  gujarat: { template: "gujarat", scaleX: 1.18, scaleY: 1.02, label: "GJ" },
  haryana: { template: "compact", scaleX: 0.95, scaleY: 0.9, label: "HR" },
  "himachal-pradesh": { template: "hill", scaleX: 1.08, scaleY: 0.72, label: "HP" },
  jharkhand: { template: "compact", label: "JH" },
  karnataka: { template: "coastalWest", scaleX: 1, scaleY: 1.18, label: "KA" },
  kerala: { template: "kerala", scaleX: 0.7, scaleY: 1.35, label: "KL" },
  "madhya-pradesh": { template: "madhya", scaleX: 1.12, scaleY: 0.98, label: "MP" },
  maharashtra: { template: "wide", scaleX: 1.2, scaleY: 0.88, label: "MH" },
  manipur: { template: "compact", scaleX: 0.82, scaleY: 0.9, label: "MN" },
  meghalaya: { template: "hill", scaleX: 1.08, scaleY: 0.62, label: "ML" },
  mizoram: { template: "vertical", scaleX: 0.72, scaleY: 1.12, label: "MZ" },
  nagaland: { template: "hill", scaleX: 0.96, scaleY: 0.7, label: "NL" },
  odisha: { template: "coastalEast", scaleX: 0.96, scaleY: 1.04, label: "OD" },
  punjab: { template: "compact", scaleX: 0.95, scaleY: 0.82, label: "PB" },
  rajasthan: { template: "rajasthan", scaleX: 1.2, scaleY: 1.04, label: "RJ" },
  sikkim: { template: "compact", scaleX: 0.72, scaleY: 0.72, label: "SK" },
  "tamil-nadu": { template: "tamil", scaleX: 0.95, scaleY: 1.16, label: "TN" },
  telangana: { template: "compact", scaleX: 0.98, scaleY: 0.96, label: "TS" },
  tripura: { template: "vertical", scaleX: 0.7, scaleY: 0.9, label: "TR" },
  "uttar-pradesh": { template: "up", scaleX: 1.25, scaleY: 0.82, label: "UP" },
  uttarakhand: { template: "hill", scaleX: 0.94, scaleY: 0.7, label: "UK" },
  "west-bengal": { template: "bengal", scaleX: 0.84, scaleY: 1.14, label: "WB" },
  chandigarh: { template: "compact", scaleX: 0.62, scaleY: 0.62, label: "CH" },
  "dadra-daman-diu": { template: "compact", scaleX: 0.74, scaleY: 0.58, label: "DD" },
  delhi: { template: "compact", scaleX: 0.72, scaleY: 0.72, label: "DL" },
  "jammu-kashmir": { template: "hill", scaleX: 1.15, scaleY: 0.76, label: "JK" },
  ladakh: { template: "hill", scaleX: 1.28, scaleY: 0.74, label: "LA" },
  puducherry: { template: "compact", scaleX: 0.62, scaleY: 0.7, label: "PY" },
  "andaman-nicobar": { template: "island", label: "AN" },
  lakshadweep: { template: "island", label: "LD" }
};

const REGION_MAP_SHAPES = {
  "jammu-kashmir": [
    [
      [0.3, 0.11],
      [0.39, 0.08],
      [0.48, 0.13],
      [0.48, 0.2],
      [0.39, 0.23],
      [0.3, 0.19],
      [0.27, 0.14]
    ]
  ],
  ladakh: [
    [
      [0.46, 0.05],
      [0.58, 0.04],
      [0.68, 0.09],
      [0.67, 0.16],
      [0.57, 0.19],
      [0.49, 0.15],
      [0.44, 0.11]
    ]
  ],
  "himachal-pradesh": [
    [
      [0.37, 0.22],
      [0.48, 0.2],
      [0.56, 0.24],
      [0.52, 0.3],
      [0.42, 0.3],
      [0.36, 0.26]
    ]
  ],
  punjab: [
    [
      [0.3, 0.24],
      [0.39, 0.23],
      [0.42, 0.3],
      [0.36, 0.34],
      [0.28, 0.31]
    ]
  ],
  chandigarh: [
    [
      [0.384, 0.263],
      [0.402, 0.264],
      [0.403, 0.282],
      [0.385, 0.283]
    ]
  ],
  haryana: [
    [
      [0.38, 0.3],
      [0.48, 0.3],
      [0.51, 0.36],
      [0.45, 0.4],
      [0.36, 0.36]
    ]
  ],
  delhi: [
    [
      [0.472, 0.358],
      [0.492, 0.362],
      [0.49, 0.386],
      [0.47, 0.382]
    ]
  ],
  uttarakhand: [
    [
      [0.49, 0.25],
      [0.59, 0.26],
      [0.63, 0.32],
      [0.58, 0.37],
      [0.49, 0.35],
      [0.46, 0.3]
    ]
  ],
  rajasthan: [
    [
      [0.18, 0.32],
      [0.31, 0.29],
      [0.41, 0.34],
      [0.43, 0.44],
      [0.37, 0.53],
      [0.25, 0.56],
      [0.16, 0.49],
      [0.13, 0.39]
    ]
  ],
  gujarat: [
    [
      [0.14, 0.52],
      [0.24, 0.49],
      [0.34, 0.56],
      [0.35, 0.66],
      [0.25, 0.73],
      [0.17, 0.68],
      [0.11, 0.6]
    ],
    [
      [0.1, 0.5],
      [0.17, 0.48],
      [0.21, 0.54],
      [0.14, 0.56]
    ]
  ],
  "dadra-daman-diu": [
    [
      [0.305, 0.632],
      [0.326, 0.636],
      [0.323, 0.66],
      [0.302, 0.655]
    ],
    [
      [0.255, 0.61],
      [0.272, 0.612],
      [0.269, 0.628],
      [0.252, 0.626]
    ]
  ],
  goa: [
    [
      [0.362, 0.722],
      [0.385, 0.73],
      [0.378, 0.765],
      [0.356, 0.756]
    ]
  ],
  "madhya-pradesh": [
    [
      [0.38, 0.44],
      [0.49, 0.41],
      [0.61, 0.46],
      [0.62, 0.55],
      [0.54, 0.62],
      [0.41, 0.6],
      [0.34, 0.52]
    ]
  ],
  "uttar-pradesh": [
    [
      [0.48, 0.35],
      [0.62, 0.35],
      [0.71, 0.4],
      [0.69, 0.47],
      [0.58, 0.49],
      [0.47, 0.44],
      [0.42, 0.39]
    ]
  ],
  bihar: [
    [
      [0.69, 0.4],
      [0.8, 0.39],
      [0.83, 0.45],
      [0.76, 0.5],
      [0.68, 0.46]
    ]
  ],
  sikkim: [
    [
      [0.744, 0.33],
      [0.776, 0.34],
      [0.77, 0.382],
      [0.738, 0.372]
    ]
  ],
  "west-bengal": [
    [
      [0.756, 0.45],
      [0.823, 0.46],
      [0.84, 0.54],
      [0.794, 0.62],
      [0.768, 0.7],
      [0.724, 0.635],
      [0.742, 0.54]
    ],
    [
      [0.732, 0.36],
      [0.77, 0.37],
      [0.772, 0.43],
      [0.738, 0.432]
    ]
  ],
  jharkhand: [
    [
      [0.65, 0.49],
      [0.745, 0.49],
      [0.766, 0.56],
      [0.694, 0.606],
      [0.62, 0.56]
    ]
  ],
  chhattisgarh: [
    [
      [0.55, 0.545],
      [0.655, 0.57],
      [0.675, 0.67],
      [0.61, 0.725],
      [0.53, 0.65]
    ]
  ],
  odisha: [
    [
      [0.65, 0.585],
      [0.756, 0.612],
      [0.777, 0.696],
      [0.69, 0.755],
      [0.604, 0.705]
    ]
  ],
  maharashtra: [
    [
      [0.32, 0.575],
      [0.46, 0.58],
      [0.57, 0.65],
      [0.545, 0.735],
      [0.41, 0.735],
      [0.32, 0.68]
    ]
  ],
  telangana: [
    [
      [0.505, 0.662],
      [0.615, 0.682],
      [0.635, 0.772],
      [0.545, 0.798],
      [0.478, 0.732]
    ]
  ],
  "andhra-pradesh": [
    [
      [0.584, 0.742],
      [0.724, 0.724],
      [0.778, 0.8],
      [0.69, 0.858],
      [0.57, 0.832],
      [0.522, 0.78]
    ]
  ],
  karnataka: [
    [
      [0.382, 0.696],
      [0.514, 0.718],
      [0.552, 0.824],
      [0.492, 0.89],
      [0.386, 0.846],
      [0.338, 0.762]
    ]
  ],
  kerala: [
    [
      [0.42, 0.832],
      [0.472, 0.862],
      [0.485, 0.956],
      [0.445, 0.985],
      [0.398, 0.912]
    ]
  ],
  "tamil-nadu": [
    [
      [0.502, 0.836],
      [0.602, 0.824],
      [0.654, 0.912],
      [0.59, 0.99],
      [0.492, 0.952]
    ]
  ],
  puducherry: [
    [
      [0.602, 0.868],
      [0.624, 0.872],
      [0.621, 0.898],
      [0.6, 0.894]
    ],
    [
      [0.662, 0.792],
      [0.684, 0.796],
      [0.68, 0.818],
      [0.66, 0.814]
    ]
  ],
  assam: [
    [
      [0.77, 0.4],
      [0.87, 0.37],
      [0.945, 0.402],
      [0.89, 0.472],
      [0.79, 0.472],
      [0.728, 0.44]
    ]
  ],
  meghalaya: [
    [
      [0.772, 0.462],
      [0.862, 0.462],
      [0.872, 0.512],
      [0.78, 0.515]
    ]
  ],
  "arunachal-pradesh": [
    [
      [0.82, 0.29],
      [0.94, 0.26],
      [0.992, 0.31],
      [0.96, 0.374],
      [0.858, 0.386],
      [0.79, 0.35]
    ]
  ],
  nagaland: [
    [
      [0.912, 0.392],
      [0.974, 0.404],
      [0.982, 0.472],
      [0.922, 0.486]
    ]
  ],
  manipur: [
    [
      [0.888, 0.482],
      [0.952, 0.494],
      [0.952, 0.565],
      [0.888, 0.555]
    ]
  ],
  mizoram: [
    [
      [0.858, 0.548],
      [0.924, 0.568],
      [0.914, 0.652],
      [0.848, 0.632]
    ]
  ],
  tripura: [
    [
      [0.81, 0.522],
      [0.862, 0.532],
      [0.858, 0.592],
      [0.81, 0.582]
    ]
  ],
  "andaman-nicobar": [
    [
      [0.805, 0.765],
      [0.823, 0.782],
      [0.818, 0.81],
      [0.797, 0.8]
    ],
    [
      [0.818, 0.835],
      [0.838, 0.852],
      [0.832, 0.884],
      [0.81, 0.867]
    ],
    [
      [0.828, 0.905],
      [0.848, 0.922],
      [0.842, 0.954],
      [0.82, 0.937]
    ],
    [
      [0.84, 0.972],
      [0.856, 0.984],
      [0.85, 0.998],
      [0.833, 0.992]
    ]
  ],
  lakshadweep: [
    [
      [0.215, 0.812],
      [0.229, 0.824],
      [0.223, 0.842],
      [0.207, 0.832]
    ],
    [
      [0.204, 0.862],
      [0.218, 0.874],
      [0.212, 0.892],
      [0.196, 0.882]
    ],
    [
      [0.222, 0.918],
      [0.236, 0.93],
      [0.23, 0.948],
      [0.214, 0.938]
    ]
  ]
};

const fallbackData = {
  opponentParties: [
    { name: "Chai Biscuit Front", color: "#2f7de1", symbol: "cup" },
    { name: "WiFi Vikas Dal", color: "#13a99a", symbol: "wheel" },
    { name: "Homework Mukti Morcha", color: "#f4c542", symbol: "kite" }
  ],
  randomEvents: [
    {
      title: "Mega rally speaker boost",
      effect: "speedUp",
      copy: "Dhol team got louder. Speed up for a bit.",
      impact: { support: 3, power: 1, funds: -2 }
    },
    {
      title: "Comedy raid on poster stock",
      effect: "raid",
      copy: "Officials counted every sticker. Funds and reputation take a hit.",
      impact: { funds: -14, reputation: -3, power: -1 }
    },
    {
      title: "Tea break at nukkad",
      effect: "teaBreak",
      copy: "Volunteers paused for chai. Speed dips, public support smiles.",
      impact: { support: 4, funds: -2, reputation: 1 }
    },
    {
      title: "Poster printer jam",
      effect: "speedDown",
      copy: "The printer needs a thappad. Speed drops for a bit.",
      impact: { funds: -5, reputation: -1 }
    },
    {
      title: "Tea stall debate won",
      effect: "claimBurst",
      copy: "Local voters liked the joke. Nearby influence grows.",
      impact: { support: 5, reputation: 2 }
    },
    {
      title: "Sticker wave",
      effect: "claimLine",
      copy: "Volunteers pasted stickers across the route.",
      impact: { funds: -4, power: 2 }
    },
    {
      title: "Rain delay",
      effect: "neutral",
      copy: "Everyone waited under the same tent. No damage, only drama.",
      impact: { reputation: 1 }
    },
    {
      title: "Meme slogan trending",
      effect: "supportSwing",
      copy: "The slogan became a reel caption. Public support jumps.",
      impact: { support: 6, reputation: 1 }
    },
    {
      title: "Donation box full",
      effect: "fundsUp",
      copy: "Small donors filled the campaign dabba.",
      impact: { funds: 12, reputation: 1 }
    },
    {
      title: "Meme wave",
      effect: "memeWave",
      copy: "Your slogan became a family group sticker. Support jumps.",
      impact: { support: 7, reputation: 2 }
    },
    {
      title: "Dhol boost",
      effect: "dholBoost",
      copy: "The dhol team found perfect rhythm. Yatra speed rises.",
      impact: { support: 3, power: 1, funds: -2 }
    },
    {
      title: "Poster rain",
      effect: "posterRain",
      copy: "Volunteers showered the booth lane with posters. Influence spreads.",
      impact: { power: 3, funds: -5 }
    }
  ],
  blockedTerms: [
    "bjp",
    "bharatiya janata party",
    "congress",
    "inc",
    "aap",
    "aam aadmi party",
    "aimim",
    "cpi",
    "cpim",
    "ljp",
    "modi",
    "narendra",
    "rahul",
    "gandhi",
    "sonia",
    "priyanka",
    "kharge",
    "kejriwal",
    "yogi",
    "amit shah",
    "shivsena",
    "shiv sena",
    "tmc",
    "trinamool",
    "samajwadi",
    "bahujan",
    "bsp",
    "dmk",
    "aiadmk",
    "ncp",
    "jdu",
    "rjd",
    "brs",
    "trs",
    "tdp",
    "ysr",
    "janata dal",
    "rss",
    "vhp",
    "owaisi",
    "nitish",
    "mamata",
    "stalin",
    "akhilesh",
    "mayawati",
    "uddhav",
    "shinde",
    "pawar",
    "lalu",
    "naidu",
    "bhagwat",
    "hindu",
    "muslim",
    "islam",
    "hindutva",
    "sanatan",
    "sikh",
    "christian",
    "mandir",
    "masjid",
    "temple",
    "mosque",
    "church",
    "gurdwara",
    "dalit",
    "brahmin",
    "rajput",
    "yadav",
    "khalistan",
    "jihad",
    "pakistan",
    "china",
    "andhbhakt",
    "bhakt",
    "sanghi",
    "terror",
    "violence",
    "kill",
    "nazi",
    "bomb",
    "gun",
    "riot",
    "attack",
    "hate",
    "abuse",
    "slur",
    "porn",
    "rape"
  ]
};

const DECISIONS = {
  teaRally: {
    label: "Tea rally",
    cost: { funds: 8 },
    impact: { support: 7, reputation: 1 },
    toast: "Tea rally clicked. Support is warming up."
  },
  posterWave: {
    label: "Poster wave",
    cost: { funds: 14 },
    impact: { support: 2, power: 3 },
    claimRadius: 3.6,
    toast: "Poster wave rolled out. Nearby booths turn your color."
  },
  donorLunch: {
    label: "Donor lunch",
    cost: { reputation: 3 },
    impact: { funds: 18, power: 2, support: -1 },
    toast: "Donor lunch done. Funds up, public mood watches closely."
  }
};

const DEFAULT_NETA = {
  support: 42,
  funds: 68,
  power: 16,
  reputation: 52,
  decisionClock: 0
};

const canvas = document.querySelector("#gameCanvas");
const ctx = canvas.getContext("2d");
const setupModal = document.querySelector("#setupModal");
const regionModal = document.querySelector("#regionModal");
const resultModal = document.querySelector("#resultModal");
const confirmPanel = document.querySelector("#confirmPanel");
const confirmTitle = document.querySelector("#confirmTitle");
const confirmCopy = document.querySelector("#confirmCopy");
const partyNameInput = document.querySelector("#partyNameInput");
const sloganInput = document.querySelector("#sloganInput");
const colorInput = document.querySelector("#colorInput");
const symbolInput = document.querySelector("#symbolInput");
const nameError = document.querySelector("#nameError");
const startBtn = document.querySelector("#startBtn");
const quickDemoBtn = document.querySelector("#quickDemoBtn");
const pitchBtn = document.querySelector("#pitchBtn");
const pitchBtnSetup = document.querySelector("#pitchBtnSetup");
const pitchModal = document.querySelector("#pitchModal");
const closePitchBtn = document.querySelector("#closePitchBtn");
const restartBtn = document.querySelector("#restartBtn");
const nextRegionBtn = document.querySelector("#nextRegionBtn");
const shareBtn = document.querySelector("#shareBtn");
const posterBtn = document.querySelector("#posterBtn");
const boostBtn = document.querySelector("#boostBtn");
const pauseBtn = document.querySelector("#pauseBtn");
const openMapBtn = document.querySelector("#openMapBtn");
const installBtn = document.querySelector("#installBtn");
const resetProgressBtn = document.querySelector("#resetProgressBtn");
const confirmRegionBtn = document.querySelector("#confirmRegionBtn");
const cancelRegionBtn = document.querySelector("#cancelRegionBtn");
const influenceStat = document.querySelector("#influenceStat");
const timeStat = document.querySelector("#timeStat");
const eventStat = document.querySelector("#eventStat");
const regionStat = document.querySelector("#regionStat");
const toast = document.querySelector("#toast");
const feedList = document.querySelector("#feedList");
const partySwatch = document.querySelector("#partySwatch");
const partyNamePreview = document.querySelector("#partyNamePreview");
const partySloganPreview = document.querySelector("#partySloganPreview");
const resultHeadline = document.querySelector("#resultHeadline");
const resultCopy = document.querySelector("#resultCopy");
const resultStamp = document.querySelector("#resultStamp");
const resultSymbol = document.querySelector("#resultSymbol");
const activeRegionCopy = document.querySelector("#activeRegionCopy");
const mobileHintCopy = document.querySelector("#mobileHintCopy");
const onboardingOverlay = document.querySelector("#onboardingOverlay");
const onboardingBadge = document.querySelector("#onboardingBadge");
const onboardingTitle = document.querySelector("#onboardingTitle");
const onboardingCopy = document.querySelector("#onboardingCopy");
const onboardingNextBtn = document.querySelector("#onboardingNextBtn");
const onboardingSkipBtn = document.querySelector("#onboardingSkipBtn");
const miniRegionGrid = document.querySelector("#miniRegionGrid");
const regionGrid = document.querySelector("#regionGrid");
const regionModalCopy = document.querySelector("#regionModalCopy");
const publicSupportStat = document.querySelector("#publicSupportStat");
const fundsStat = document.querySelector("#fundsStat");
const powerStat = document.querySelector("#powerStat");
const reputationStat = document.querySelector("#reputationStat");
const mandateMood = document.querySelector("#mandateMood");
const decisionButtons = document.querySelectorAll("[data-decision]");

const state = {
  data: fallbackData,
  owner: new Uint8Array(COLS * ROWS),
  trail: new Uint8Array(COLS * ROWS),
  regionMask: new Uint8Array(COLS * ROWS),
  activePolygon: [],
  demoMode: DEMO_FROM_QUERY,
  onboardingStep: 0,
  screenShake: 0,
  screenShakePower: 0,
  mode: "setup",
  party: {
    name: "Momo Lovers Party",
    slogan: "Vote for extra chutney",
    color: "#f05d23",
    symbol: "star"
  },
  player: null,
  opponents: [],
  supporters: [],
  ambientPeople: [],
  eventScenes: [],
  conversionBursts: [],
  claimBursts: [],
  supportersConverted: 0,
  campaign: {
    activeRegionId: null,
    pendingRegionId: null,
    completed: {},
    lastWonRegionId: null,
    nationalWon: false
  },
  neta: { ...DEFAULT_NETA },
  keys: new Set(),
  lastTime: 0,
  timeLeft: ROUND_SECONDS,
  roundElapsed: 0,
  roundStarted: false,
  paused: false,
  eventClock: 12,
  boostClock: 0,
  speedMul: 1,
  toastClock: 0,
  influence: 0,
  cellSize: 12,
  offsetX: 0,
  offsetY: 0,
  canvasWidth: 960,
  canvasHeight: 630,
  dpr: 1,
  mapRect: { x: 0, y: 0, width: 0, height: 0 },
  pointer: { x: 0, y: 0, active: false, lastDir: null },
  touchCue: null,
  dangerCue: null,
  dangerHapticClock: 0,
  resetProgressTimer: null,
  installPromptEvent: null,
  appInstalled: false,
  audioContext: null,
  audioUnlocked: false,
  shareText: "",
  resultSummary: null
};

async function loadGameData() {
  try {
    const response = await fetch("data/game-data.json", { cache: "no-store" });
    if (!response.ok) throw new Error("Data file unavailable");
    state.data = await response.json();
  } catch {
    state.data = fallbackData;
  }
}

function index(x, y) {
  return y * COLS + x;
}

function clamp(value, min, max) {
  return Math.max(min, Math.min(max, value));
}

function clampNetaValue(key, value) {
  const max = key === "funds" ? 140 : 100;
  return clamp(Math.round(Number(value) || 0), 0, max);
}

function hydrateNeta(raw) {
  return {
    support: clampNetaValue("support", raw?.support ?? DEFAULT_NETA.support),
    funds: clampNetaValue("funds", raw?.funds ?? DEFAULT_NETA.funds),
    power: clampNetaValue("power", raw?.power ?? DEFAULT_NETA.power),
    reputation: clampNetaValue("reputation", raw?.reputation ?? DEFAULT_NETA.reputation),
    decisionClock: 0
  };
}

function mandateScore() {
  const supportBonus = (state.neta.support - 50) * 0.14;
  const repBonus = (state.neta.reputation - 50) * 0.1;
  const powerBonus = state.neta.power * 0.04;
  return clamp(Math.round(state.influence + supportBonus + repBonus + powerBonus), 0, 100);
}

function mandateMoodLabel() {
  const score = mandateScore();
  if (score >= 72) return "Wave";
  if (score >= 55) return "Mandate";
  if (score >= 42) return "Fight";
  return "Tight";
}

function canAfford(cost = {}) {
  return Object.entries(cost).every(([key, value]) => state.neta[key] >= value);
}

function updateDecisionButtons() {
  decisionButtons.forEach((button) => {
    const decision = DECISIONS[button.dataset.decision];
    const unavailable =
      !decision || state.mode !== "playing" || !state.roundStarted || state.neta.decisionClock > 0 || !canAfford(decision.cost);
    button.disabled = unavailable;
  });
}

function updateNetaPanel() {
  if (!publicSupportStat) return;
  publicSupportStat.textContent = state.neta.support;
  fundsStat.textContent = state.neta.funds;
  powerStat.textContent = state.neta.power;
  reputationStat.textContent = state.neta.reputation;
  mandateMood.textContent = `${mandateMoodLabel()} ${mandateScore()}`;
  updateDecisionButtons();
}

function adjustNeta(delta = {}, save = true) {
  for (const key of ["support", "funds", "power", "reputation"]) {
    if (typeof delta[key] === "number") {
      state.neta[key] = clampNetaValue(key, state.neta[key] + delta[key]);
    }
  }
  updateNetaPanel();
  if (save) saveCampaignProgress();
}

function combineResourceChanges(...changes) {
  const combined = {};
  for (const change of changes) {
    if (!change) continue;
    for (const [key, value] of Object.entries(change)) {
      combined[key] = (combined[key] || 0) + value;
    }
  }
  return combined;
}

function normalizeName(value) {
  return value
    .toLowerCase()
    .replace(/[^a-z0-9 ]+/g, " ")
    .replace(/\s+/g, " ")
    .trim();
}

function compactSafetyText(value, collapseRepeats = true) {
  const leetFixed = value
    .toLowerCase()
    .replace(/0/g, "o")
    .replace(/1/g, "i")
    .replace(/3/g, "e")
    .replace(/4/g, "a")
    .replace(/5/g, "s")
    .replace(/7/g, "t")
    .replace(/8/g, "b");
  const compact = normalizeName(leetFixed).replace(/\s+/g, "");
  return collapseRepeats ? compact.replace(/(.)\1{2,}/g, "$1$1") : compact;
}

function findBlockedTerm(value) {
  const normalized = normalizeName(value);
  const compact = compactSafetyText(value);
  const blocked = state.data.blockedTerms || fallbackData.blockedTerms;
  return blocked.find((term) => {
    const safeTerm = normalizeName(term);
    const compactTerm = compactSafetyText(term, false);
    if (!safeTerm || !compactTerm) return false;
    return normalized.includes(safeTerm) || compact.includes(compactTerm);
  });
}

function validateSafeText(value, label, minLength) {
  const raw = value.trim().toLowerCase();
  if (!/^[a-z0-9 ]+$/.test(raw)) {
    return "English letters, numbers, aur spaces use karo.";
  }
  const normalized = normalizeName(raw);
  if (normalized.length < minLength) {
    return `${label} thoda bada rakho.`;
  }
  if (/(.)\1{4,}/.test(normalized.replace(/\s+/g, ""))) {
    return `${label} me repeated spam letters kam rakho.`;
  }
  const matched = findBlockedTerm(normalized);
  if (matched) {
    return "Fictional comedy text rakho. Real politics, hate, abuse, ya sensitive terms blocked hain.";
  }
  return "";
}

function validatePartyName(name) {
  return validateSafeText(name, "Party name", 3);
}

function validateSlogan(slogan) {
  if (!slogan.trim()) return "";
  return validateSafeText(slogan, "Slogan", 4);
}

function setPartyPreview() {
  partySwatch.style.background = state.party.color;
  partyNamePreview.textContent = state.party.name;
  partySloganPreview.textContent = state.party.slogan;
}

function applyPartyPreset(preset = DEMO_PARTY) {
  partyNameInput.value = preset.name;
  sloganInput.value = preset.slogan;
  colorInput.value = preset.color;
  symbolInput.value = preset.symbol;
  state.party = { ...preset };
  setPartyPreview();
  if (nameError) nameError.textContent = "";
}

function updateOnboarding() {
  if (!onboardingOverlay) return;
  const step = ONBOARDING_STEPS[state.onboardingStep] || ONBOARDING_STEPS[0];
  onboardingBadge.textContent = `${state.onboardingStep + 1}/${ONBOARDING_STEPS.length}`;
  onboardingTitle.textContent = step.title;
  onboardingCopy.textContent = step.copy;
  onboardingNextBtn.textContent = state.onboardingStep === ONBOARDING_STEPS.length - 1 ? "Done" : "Next";
  onboardingOverlay.querySelectorAll(".onboarding-steps span").forEach((dot, indexValue) => {
    dot.classList.toggle("is-active", indexValue <= state.onboardingStep);
  });
}

function isOnboardingVisible() {
  return Boolean(onboardingOverlay && !onboardingOverlay.hidden);
}

function showOnboarding(force = false) {
  if (!onboardingOverlay) return;
  if (!force) {
    try {
      if (localStorage.getItem(ONBOARDING_KEY) === "yes") return;
    } catch {
      // Onboarding is optional if storage is unavailable.
    }
  }
  state.onboardingStep = 0;
  updateOnboarding();
  if (confirmPanel) confirmPanel.hidden = true;
  onboardingOverlay.hidden = false;
}

function hideOnboarding(markSeen = true) {
  if (!onboardingOverlay) return;
  onboardingOverlay.hidden = true;
  if (state.mode === "confirm" && state.campaign.pendingRegionId && confirmPanel) {
    confirmPanel.hidden = false;
  }
  if (!markSeen) return;
  try {
    localStorage.setItem(ONBOARDING_KEY, "yes");
  } catch {
    // Storage can fail in strict browser modes; the overlay can still close.
  }
}

function nextOnboardingStep() {
  if (state.onboardingStep >= ONBOARDING_STEPS.length - 1) {
    hideOnboarding();
    return;
  }
  state.onboardingStep += 1;
  updateOnboarding();
}

function openPitchCard() {
  pitchModal?.classList.add("is-open");
}

function closePitchCard() {
  pitchModal?.classList.remove("is-open");
}

function activateQuickDemo({ fromUrl = false } = {}) {
  state.demoMode = true;
  applyPartyPreset(DEMO_PARTY);
  state.neta = { support: 58, funds: 96, power: 24, reputation: 62, decisionClock: 0 };
  updateNetaPanel();
  setupModal.classList.remove("is-open");
  regionModal.classList.remove("is-open");
  resultModal.classList.remove("is-open");
  if (MAP_QA_FROM_QUERY) {
    state.mode = "map";
    state.campaign.pendingRegionId = null;
    if (confirmPanel) confirmPanel.hidden = true;
    hideOnboarding(false);
    renderRegionHub();
    addFeed("Map QA mode: full India state/UT map is visible without onboarding overlays.");
    state.toastClock = 0;
    toast.classList.remove("is-visible");
    return;
  }
  showRegionPrompt(DEMO_REGION_ID, { silent: fromUrl });
  renderRegionHub();
  showOnboarding(true);
  addFeed("Quick Demo ready: preset party, UP arena, installable PWA, safe fictional content.");
  if (fromUrl) {
    state.toastClock = 0;
    toast.classList.remove("is-visible");
  } else {
    showToast("Quick Demo ready. Press OK to open the pitch arena.");
  }
}

function symbolLabel(symbol) {
  const labels = {
    star: "STAR",
    kite: "KITE",
    mic: "MIC",
    cup: "CUP",
    wheel: "WHEEL"
  };
  return labels[symbol] || "NETA";
}

function addFeed(text) {
  const item = document.createElement("li");
  item.textContent = text;
  feedList.prepend(item);
  while (feedList.children.length > 8) {
    feedList.lastElementChild.remove();
  }
}

function showToast(message) {
  toast.textContent = message;
  toast.classList.add("is-visible");
  state.toastClock = 2.4;
}

function triggerHaptic(pattern = 10) {
  if (!("vibrate" in navigator)) return;
  try {
    navigator.vibrate(pattern);
  } catch {
    // Haptics are a mobile polish layer only; unsupported devices can ignore them.
  }
}

function triggerScreenShake(power = 4, duration = 0.2) {
  state.screenShake = Math.max(state.screenShake, duration);
  state.screenShakePower = Math.max(state.screenShakePower, power);
}

function updateScreenShake(dt) {
  if (state.screenShake <= 0) {
    if (canvas.style.transform) canvas.style.transform = "";
    state.screenShakePower = 0;
    return;
  }
  state.screenShake = Math.max(0, state.screenShake - dt);
  const power = state.screenShakePower * (state.screenShake / Math.max(0.001, state.screenShake + dt));
  const x = Math.sin((state.lastTime || 0) * 0.08) * power;
  const y = Math.cos((state.lastTime || 0) * 0.07) * power * 0.65;
  canvas.style.transform = `translate(${x.toFixed(2)}px, ${y.toFixed(2)}px)`;
}

function ensureAudio() {
  if (state.audioUnlocked) return;
  const AudioCtor = window.AudioContext || window.webkitAudioContext;
  if (!AudioCtor) return;
  state.audioContext = state.audioContext || new AudioCtor();
  if (state.audioContext.state === "suspended") {
    state.audioContext.resume();
  }
  state.audioUnlocked = true;
}

function playSound(kind) {
  if (!state.audioUnlocked || !state.audioContext) return;
  const ctxAudio = state.audioContext;
  const now = ctxAudio.currentTime;
  const patterns = {
    tap: [420, 0.045, 0.05],
    rally: [240, 0.09, 0.09, 360, 0.09, 0.07],
    convert: [520, 0.07, 0.08, 760, 0.11, 0.06],
    dhol: [150, 0.055, 0.1, 110, 0.055, 0.08, 210, 0.055, 0.09, 120, 0.075, 0.07],
    meme: [660, 0.055, 0.05, 880, 0.055, 0.05, 990, 0.09, 0.045],
    poster: [420, 0.045, 0.045, 520, 0.045, 0.04, 620, 0.045, 0.035, 720, 0.08, 0.03],
    loop: [300, 0.08, 0.08, 460, 0.08, 0.06, 620, 0.1, 0.05],
    event: [180, 0.06, 0.08, 260, 0.12, 0.06],
    win: [392, 0.1, 0.08, 523, 0.1, 0.07, 659, 0.18, 0.06],
    lose: [260, 0.12, 0.07, 180, 0.18, 0.06]
  };
  const pattern = patterns[kind] || patterns.tap;
  let cursor = now;
  for (let i = 0; i < pattern.length; i += 3) {
    const frequency = pattern[i];
    const duration = pattern[i + 1];
    const gain = pattern[i + 2];
    const oscillator = ctxAudio.createOscillator();
    const volume = ctxAudio.createGain();
    oscillator.type = kind === "rally" || kind === "dhol" ? "square" : kind === "meme" ? "sine" : "triangle";
    oscillator.frequency.setValueAtTime(frequency, cursor);
    volume.gain.setValueAtTime(0.0001, cursor);
    volume.gain.exponentialRampToValueAtTime(gain, cursor + 0.01);
    volume.gain.exponentialRampToValueAtTime(0.0001, cursor + duration);
    oscillator.connect(volume);
    volume.connect(ctxAudio.destination);
    oscillator.start(cursor);
    oscillator.stop(cursor + duration + 0.02);
    cursor += duration * 0.92;
  }
}

function getActiveRegion() {
  return REGIONS.find((region) => region.id === state.campaign.activeRegionId) || null;
}

function getResumableActiveRegion() {
  const activeRegion = getActiveRegion();
  if (!activeRegion || state.campaign.completed[activeRegion.id]) return null;
  return activeRegion;
}

function completedRegionCount() {
  return REGIONS.filter((region) => Boolean(state.campaign.completed[region.id])).length;
}

function isIndiaComplete() {
  return completedRegionCount() === REGIONS.length;
}

function loadCampaignProgress() {
  try {
    const raw = localStorage.getItem(PROGRESS_KEY);
    if (!raw) return;
    const parsed = JSON.parse(raw);
    state.campaign.activeRegionId = parsed.activeRegionId || null;
    state.campaign.completed = parsed.completed && typeof parsed.completed === "object" ? parsed.completed : {};
    state.campaign.lastWonRegionId = parsed.lastWonRegionId || null;
    state.campaign.nationalWon = Boolean(parsed.nationalWon) || REGIONS.every((region) => parsed.completed?.[region.id]);
    state.neta = hydrateNeta(parsed.neta);
  } catch {
    state.campaign.activeRegionId = null;
    state.campaign.completed = {};
    state.campaign.lastWonRegionId = null;
    state.campaign.nationalWon = false;
    state.neta = hydrateNeta();
  }
}

function saveCampaignProgress() {
  const payload = {
    activeRegionId: state.campaign.activeRegionId,
    completed: state.campaign.completed,
    lastWonRegionId: state.campaign.lastWonRegionId,
    nationalWon: state.campaign.nationalWon,
    neta: {
      support: state.neta.support,
      funds: state.neta.funds,
      power: state.neta.power,
      reputation: state.neta.reputation
    }
  };
  localStorage.setItem(PROGRESS_KEY, JSON.stringify(payload));
}

function updateMobileHint() {
  if (!mobileHintCopy) return;
  const wonCount = completedRegionCount();
  const activeRegion = getActiveRegion();
  if (state.mode === "playing" && activeRegion) {
    mobileHintCopy.textContent = state.roundStarted
      ? "Swipe/tap anywhere in the arena to turn. Rally gives a short speed burst."
      : `${activeRegion.name}: tap or swipe inside the arena to start the yatra.`;
    return;
  }
  if (state.mode === "result") {
    mobileHintCopy.textContent = "Result card ke baad Next State se India map par wapas jao.";
    return;
  }
  mobileHintCopy.textContent = wonCount > 0
    ? `${wonCount}/${REGIONS.length} regions won. Reset clears local test progress.`
    : "On phone: tap a flag, OK, then swipe inside the state arena.";
}

function disarmResetProgressButton() {
  if (!resetProgressBtn) return;
  resetProgressBtn.textContent = "Reset";
  resetProgressBtn.classList.remove("is-armed");
  resetProgressBtn.dataset.confirming = "false";
  if (state.resetProgressTimer) {
    window.clearTimeout(state.resetProgressTimer);
    state.resetProgressTimer = null;
  }
}

function resetCampaignProgress() {
  try {
    localStorage.removeItem(PROGRESS_KEY);
  } catch {
    // Local storage can fail in strict browser modes; reset the in-memory campaign anyway.
  }
  disarmResetProgressButton();
  state.campaign.activeRegionId = null;
  state.campaign.pendingRegionId = null;
  state.campaign.completed = {};
  state.campaign.lastWonRegionId = null;
  state.campaign.nationalWon = false;
  state.neta = { ...DEFAULT_NETA };
  state.owner.fill(0);
  state.trail.fill(0);
  state.regionMask.fill(0);
  state.activePolygon = [];
  state.player = null;
  state.opponents = [];
  state.supporters = [];
  state.ambientPeople = [];
  state.eventScenes = [];
  state.conversionBursts = [];
  state.claimBursts = [];
  state.supportersConverted = 0;
  state.keys.clear();
  state.pointer.active = false;
  state.touchCue = null;
  state.dangerCue = null;
  state.dangerHapticClock = 0;
  state.timeLeft = ROUND_SECONDS;
  state.roundElapsed = 0;
  state.roundStarted = false;
  state.paused = false;
  state.eventClock = 12;
  state.boostClock = 0;
  state.speedMul = 1;
  state.influence = 0;
  state.shareText = "";
  state.resultSummary = null;
  feedList.innerHTML = "";
  setupModal.classList.remove("is-open");
  resultModal.classList.remove("is-open");
  regionModal.classList.remove("is-open");
  confirmPanel.hidden = true;
  pauseBtn.textContent = "Pause";
  eventStat.textContent = "Ready";
  state.mode = "map";
  renderRegionHub();
  updateStats();
  addFeed("Progress reset. Choose any state for a fresh mandate.");
  showToast("Progress reset. All flags are fresh again.");
  playSound("tap");
  triggerHaptic([8, 30, 8]);
}

function handleResetProgressClick() {
  ensureAudio();
  if (!resetProgressBtn) {
    resetCampaignProgress();
    return;
  }
  if (resetProgressBtn.dataset.confirming === "true") {
    resetCampaignProgress();
    return;
  }
  resetProgressBtn.dataset.confirming = "true";
  resetProgressBtn.textContent = "Sure?";
  resetProgressBtn.classList.add("is-armed");
  showToast("Tap Sure? to clear saved local progress.");
  triggerHaptic(8);
  if (state.resetProgressTimer) window.clearTimeout(state.resetProgressTimer);
  state.resetProgressTimer = window.setTimeout(disarmResetProgressButton, 4200);
}

function renderRegionHub() {
  const activeRegion = getActiveRegion();
  const resumableRegion = getResumableActiveRegion();
  const pendingRegion = REGIONS.find((region) => region.id === state.campaign.pendingRegionId);
  regionStat.textContent = activeRegion ? activeRegion.name : "Choose State";
  const wonCount = completedRegionCount();
  activeRegionCopy.textContent = state.campaign.nationalWon
    ? `National mandate complete: ${wonCount}/${REGIONS.length} regions. World yatra teaser unlocked.`
    : pendingRegion
      ? `${pendingRegion.name} selected. OK dabao to open the big state arena.`
      : resumableRegion
      ? `${resumableRegion.name} is active. Resume it until the mandate is won.`
      : `Touch a black flag on the India map. Red flags are won: ${wonCount}/${REGIONS.length}.`;
  if (miniRegionGrid) {
    miniRegionGrid.innerHTML = "";
    miniRegionGrid.style.setProperty("--region-color", state.party.color);
  }
  if (regionGrid) {
    regionGrid.innerHTML = "";
    regionGrid.style.setProperty("--region-color", state.party.color);
  }

  for (const region of REGIONS) {
    const won = Boolean(state.campaign.completed[region.id]);
    const active = state.campaign.activeRegionId === region.id;
    const pending = state.campaign.pendingRegionId === region.id;

    const mini = document.createElement("button");
    mini.type = "button";
    mini.className = `mini-region-cell${won ? " is-won" : ""}${active ? " is-active" : ""}${pending ? " is-pending" : ""}`;
    mini.title = `${region.name}${pending ? " selected" : won ? " won" : active ? " active" : ""}`;
    mini.setAttribute("aria-label", mini.title);
    mini.addEventListener("click", () => showRegionPrompt(region.id));
    miniRegionGrid?.append(mini);

    const button = document.createElement("button");
    button.type = "button";
    button.className = `region-btn${won ? " is-won" : ""}${active ? " is-active" : ""}${pending ? " is-pending" : ""}`;
    button.innerHTML = `${region.name}<small>${pending ? "Selected - press OK" : won ? "Won mandate" : active ? "Active campaign" : region.type}</small>`;
    button.addEventListener("click", () => showRegionPrompt(region.id));
    regionGrid?.append(button);
  }
  updateMobileHint();
  updateNetaPanel();
}

function openRegionModal() {
  setupModal.classList.remove("is-open");
  resultModal.classList.remove("is-open");
  regionModal.classList.remove("is-open");
  confirmPanel.hidden = true;
  state.campaign.pendingRegionId = null;
  renderRegionHub();
  state.mode = "map";
  updateMobileHint();
  showToast(state.campaign.nationalWon ? "National mandate complete. World yatra coming soon." : "Touch any black flag on the India map.");
}

function showRegionPrompt(regionId, options = {}) {
  const region = REGIONS.find((item) => item.id === regionId);
  if (!region) return;
  if (!options.silent) {
    ensureAudio();
    playSound("tap");
    triggerHaptic(8);
  }
  state.campaign.pendingRegionId = region.id;
  state.mode = "confirm";
  renderRegionHub();
  confirmTitle.textContent = `${region.name} election?`;
  confirmCopy.textContent = state.campaign.completed[region.id]
    ? "This mandate is already won. OK to replay this region."
    : "OK dabao, phir sirf is region ka bada outline arena khulega.";
  confirmPanel.hidden = isOnboardingVisible();
}

function selectRegion(regionId) {
  const region = REGIONS.find((item) => item.id === regionId);
  if (!region) return;
  const previous = getActiveRegion();
  state.campaign.activeRegionId = region.id;
  saveCampaignProgress();
  renderRegionHub();
  regionModal.classList.remove("is-open");
  resetGame();
  resultModal.classList.remove("is-open");
  setupModal.classList.remove("is-open");
  state.mode = "playing";
  updateMobileHint();
  const yatraCopy = previous && previous.id !== region.id ? `Paidal yatra moved from ${previous.name} to ${region.name}.` : `${region.name} campaign opened.`;
  addFeed(yatraCopy);
  showToast(yatraCopy);
}

function confirmPendingRegion() {
  if (!state.campaign.pendingRegionId) return;
  ensureAudio();
  playSound("rally");
  const regionId = state.campaign.pendingRegionId;
  state.campaign.pendingRegionId = null;
  confirmPanel.hidden = true;
  selectRegion(regionId);
}

function markActiveRegionWon() {
  const activeRegion = getActiveRegion();
  if (!activeRegion) return null;
  state.campaign.completed[activeRegion.id] = {
    wonAt: Date.now(),
    influence: state.influence,
    mandateScore: mandateScore(),
    support: state.neta.support,
    reputation: state.neta.reputation,
    party: state.party.name
  };
  state.campaign.lastWonRegionId = activeRegion.id;
  state.campaign.activeRegionId = null;
  state.campaign.nationalWon = isIndiaComplete();
  saveCampaignProgress();
  renderRegionHub();
  return activeRegion;
}

function resizeCanvas() {
  const rect = canvas.getBoundingClientRect();
  const cssWidth = Math.max(1, Math.floor(rect.width));
  const cssHeight = Math.max(1, Math.floor(rect.height));
  const isMobile = Math.min(window.innerWidth || cssWidth, cssWidth) < 720;
  const dpr = Math.min(window.devicePixelRatio || 1, isMobile ? 1.75 : 2);
  const nextWidth = Math.max(1, Math.floor(cssWidth * dpr));
  const nextHeight = Math.max(1, Math.floor(cssHeight * dpr));
  if (canvas.width !== nextWidth) canvas.width = nextWidth;
  if (canvas.height !== nextHeight) canvas.height = nextHeight;
  ctx.setTransform(dpr, 0, 0, dpr, 0, 0);
  ctx.imageSmoothingEnabled = true;
  ctx.imageSmoothingQuality = "high";
  state.canvasWidth = cssWidth;
  state.canvasHeight = cssHeight;
  state.dpr = dpr;
  state.cellSize = Math.min(cssWidth / COLS, cssHeight / ROWS);
  state.offsetX = (cssWidth - state.cellSize * COLS) / 2;
  state.offsetY = (cssHeight - state.cellSize * ROWS) / 2;
}

function hashText(value) {
  let hash = 2166136261;
  for (let i = 0; i < value.length; i += 1) {
    hash ^= value.charCodeAt(i);
    hash = Math.imul(hash, 16777619);
  }
  return hash >>> 0;
}

function seededNoise(seed, indexValue) {
  let value = seed + indexValue * 1013904223;
  value ^= value << 13;
  value ^= value >>> 17;
  value ^= value << 5;
  return ((value >>> 0) % 10000) / 10000;
}

function getRegionShapeProfile(region) {
  const profile = REGION_SHAPE_PROFILES[region?.id] || {};
  return {
    template: profile.template || (region?.type === "UT" ? "compact" : "state"),
    scaleX: profile.scaleX || 1,
    scaleY: profile.scaleY || 1,
    angle: profile.angle || 0,
    label: profile.label || ""
  };
}

function getRegionShapeLocalPoints(region, jitter = 0) {
  const profile = getRegionShapeProfile(region);
  const template = REGION_SHAPE_TEMPLATES[profile.template] || REGION_SHAPE_TEMPLATES.state;
  const seed = hashText(region?.id || "district");
  return template.map(([x, y], i) => {
    const jx = jitter ? (seededNoise(seed, i * 11 + 5) - 0.5) * jitter : 0;
    const jy = jitter ? (seededNoise(seed, i * 13 + 7) - 0.5) * jitter : 0;
    return [x + jx, y + jy];
  });
}

function transformRegionShapePoint(localX, localY, centerX, centerY, rx, ry, profile) {
  const x = localX * rx * profile.scaleX;
  const y = localY * ry * profile.scaleY;
  const cos = Math.cos(profile.angle);
  const sin = Math.sin(profile.angle);
  return {
    x: centerX + x * cos - y * sin,
    y: centerY + x * sin + y * cos
  };
}

function generateRegionPolygon(region) {
  const profile = getRegionShapeProfile(region);
  const localPoints = getRegionShapeLocalPoints(region, 0.1);
  const points = [];
  const centerX = COLS / 2;
  const centerY = ROWS / 2;
  const radiusX = region?.type === "UT" ? 20.5 : 22.5;
  const radiusY = region?.type === "UT" ? 15.6 : 16.8;
  for (const [x, y] of localPoints) {
    points.push(transformRegionShapePoint(x, y, centerX, centerY, radiusX, radiusY, profile));
  }
  return points;
}

function pointInPolygon(x, y, points) {
  if (!points.length) return true;
  let inside = false;
  for (let i = 0, j = points.length - 1; i < points.length; j = i, i += 1) {
    const xi = points[i].x;
    const yi = points[i].y;
    const xj = points[j].x;
    const yj = points[j].y;
    const intersect = yi > y !== yj > y && x < ((xj - xi) * (y - yi)) / (yj - yi || 1) + xi;
    if (intersect) inside = !inside;
  }
  return inside;
}

function isMaskedCell(x, y) {
  if (!state.activePolygon.length) return true;
  if (x < 0 || x >= COLS || y < 0 || y >= ROWS) return false;
  return state.regionMask[index(Math.floor(x), Math.floor(y))] === 1;
}

function generateRegionMask(region) {
  state.regionMask.fill(0);
  state.activePolygon = generateRegionPolygon(region);
  for (let y = 0; y < ROWS; y += 1) {
    for (let x = 0; x < COLS; x += 1) {
      if (pointInPolygon(x + 0.5, y + 0.5, state.activePolygon)) {
        state.regionMask[index(x, y)] = 1;
      }
    }
  }
}

function findMaskedSpawn(preferredX, preferredY) {
  if (isMaskedCell(preferredX, preferredY)) return { x: preferredX, y: preferredY };
  for (let r = 1; r < 22; r += 1) {
    for (let y = Math.max(1, Math.floor(preferredY - r)); y <= Math.min(ROWS - 2, Math.ceil(preferredY + r)); y += 1) {
      for (let x = Math.max(1, Math.floor(preferredX - r)); x <= Math.min(COLS - 2, Math.ceil(preferredX + r)); x += 1) {
        if (isMaskedCell(x, y)) return { x, y };
      }
    }
  }
  return { x: COLS / 2, y: ROWS / 2 };
}

function randomMaskedPoint() {
  for (let i = 0; i < 80; i += 1) {
    const x = 2 + Math.random() * (COLS - 4);
    const y = 2 + Math.random() * (ROWS - 4);
    if (isMaskedCell(x, y)) return { x, y };
  }
  return findMaskedSpawn(COLS / 2, ROWS / 2);
}

function claimRect(ownerId, cx, cy, w, h) {
  const x0 = clamp(Math.floor(cx - w / 2), 1, COLS - 2);
  const x1 = clamp(Math.floor(cx + w / 2), 1, COLS - 2);
  const y0 = clamp(Math.floor(cy - h / 2), 1, ROWS - 2);
  const y1 = clamp(Math.floor(cy + h / 2), 1, ROWS - 2);
  for (let y = y0; y <= y1; y += 1) {
    for (let x = x0; x <= x1; x += 1) {
      if (!isMaskedCell(x, y)) continue;
      state.owner[index(x, y)] = ownerId;
      state.trail[index(x, y)] = 0;
    }
  }
}

function claimDisk(ownerId, cx, cy, radius) {
  const r2 = radius * radius;
  const changed = [];
  for (let y = Math.max(1, Math.floor(cy - radius)); y <= Math.min(ROWS - 2, Math.ceil(cy + radius)); y += 1) {
    for (let x = Math.max(1, Math.floor(cx - radius)); x <= Math.min(COLS - 2, Math.ceil(cx + radius)); x += 1) {
      const dx = x - cx;
      const dy = y - cy;
      if (dx * dx + dy * dy <= r2 && isMaskedCell(x, y)) {
        const idx = index(x, y);
        if (state.owner[idx] !== ownerId) changed.push(idx);
        state.owner[idx] = ownerId;
        state.trail[idx] = 0;
      }
    }
  }
  return changed;
}

function createAgent(ownerId, name, color, symbol, x, y) {
  const squad = ownerId === 1
    ? []
    : Array.from({ length: 3 }, (_, i) => ({
        back: 1.15 + i * 0.42 + Math.random() * 0.32,
        side: (i - 1) * 0.62 + (Math.random() - 0.5) * 0.28,
        phase: Math.random() * Math.PI * 2,
        size: 0.62 + Math.random() * 0.22,
        flag: i === 1
      }));
  return {
    ownerId,
    name,
    color,
    symbol,
    x,
    y,
    dirX: ownerId % 2 === 0 ? 1 : -1,
    dirY: 0,
    speed: 4.1,
    turnClock: 0.8 + Math.random() * 0.9,
    targetX: x,
    targetY: y,
    squad,
    trailCells: [],
    trailPoints: []
  };
}

function createSupporter(source, color) {
  const angle = Math.random() * Math.PI * 2;
  const distance = 0.3 + Math.random() * 1.4;
  return {
    x: clamp(source.x + Math.cos(angle) * distance, 1, COLS - 1.01),
    y: clamp(source.y + Math.sin(angle) * distance, 1, ROWS - 1.01),
    color,
    phase: Math.random() * Math.PI * 2,
    driftX: (Math.random() - 0.5) * 2.6,
    driftY: (Math.random() - 0.5) * 2.6,
    spread: 0.7 + Math.random() * 1.9,
    followSpeed: 4.4 + Math.random() * 2.2,
    size: 0.36 + Math.random() * 0.18
  };
}

function createAmbientPeople(region) {
  const seed = hashText(region?.id || "campaign");
  const count = region?.type === "UT" ? 22 : 34;
  const roles = ["voter", "volunteer", "poster", "flag", "dhol", "camera"];
  const palette = ["#fffdf7", "#ffd166", "#bfe7ff", "#f7c68f", "#b9dfb5"];
  const people = [];

  for (let i = 0; i < count; i += 1) {
    const baseX = 4 + seededNoise(seed, i * 7 + 3) * (COLS - 8);
    const baseY = 4 + seededNoise(seed, i * 9 + 11) * (ROWS - 8);
    const spot = findMaskedSpawn(baseX, baseY);
    const role = roles[Math.floor(seededNoise(seed, i * 13 + 19) * roles.length)] || "voter";
    const color = palette[Math.floor(seededNoise(seed, i * 17 + 29) * palette.length)] || "#fffdf7";
    people.push({
      x: spot.x,
      y: spot.y,
      homeX: spot.x,
      homeY: spot.y,
      targetX: spot.x,
      targetY: spot.y,
      role,
      color,
      phase: seededNoise(seed, i * 23 + 31) * Math.PI * 2,
      wanderClock: 0.6 + seededNoise(seed, i * 31 + 37) * 2.4,
      speed: 0.45 + seededNoise(seed, i * 41 + 43) * 0.75,
      size: 0.58 + seededNoise(seed, i * 47 + 53) * 0.26
    });
  }

  return people;
}

function updateAmbientPeople(dt) {
  for (const person of state.ambientPeople) {
    person.phase += dt * (0.9 + person.size);
    person.wanderClock -= dt;
    if (person.wanderClock <= 0) {
      const angle = Math.random() * Math.PI * 2;
      const distance = person.role === "voter" ? 1.8 + Math.random() * 2.8 : 0.8 + Math.random() * 1.8;
      const targetX = person.homeX + Math.cos(angle) * distance;
      const targetY = person.homeY + Math.sin(angle) * distance;
      if (isMaskedCell(targetX, targetY)) {
        person.targetX = targetX;
        person.targetY = targetY;
      }
      person.wanderClock = 1.3 + Math.random() * 2.6;
    }

    const dx = person.targetX - person.x;
    const dy = person.targetY - person.y;
    const distance = Math.hypot(dx, dy) || 1;
    const step = Math.min(distance, person.speed * dt);
    const nextX = person.x + (dx / distance) * step;
    const nextY = person.y + (dy / distance) * step;
    if (isMaskedCell(nextX, nextY)) {
      person.x = nextX;
      person.y = nextY;
    }
  }
}

function addConversionBurst(x, y, color) {
  for (let i = 0; i < 18; i += 1) {
    const angle = Math.random() * Math.PI * 2;
    const speed = 2 + Math.random() * 5;
    state.conversionBursts.push({
      x,
      y,
      color,
      vx: Math.cos(angle) * speed,
      vy: Math.sin(angle) * speed,
      life: 0.45 + Math.random() * 0.45,
      maxLife: 0.9,
      size: 0.22 + Math.random() * 0.28
    });
  }
}

function addClaimBurst(cells, color) {
  if (!cells.length) return;
  const limit = Math.min(34, cells.length);
  const step = Math.max(1, Math.floor(cells.length / limit));
  for (let i = 0; i < cells.length; i += step) {
    const idx = cells[i];
    const x = idx % COLS;
    const y = Math.floor(idx / COLS);
    state.claimBursts.push({
      x: x + 0.5,
      y: y + 0.5,
      color,
      life: 0.55 + Math.random() * 0.35,
      maxLife: 0.9,
      delay: Math.random() * 0.22,
      size: 0.25 + Math.random() * 0.45
    });
  }
  while (state.claimBursts.length > 120) state.claimBursts.shift();
}

function addEventScene(type, x, y, color) {
  const spot = findMaskedSpawn(x, y);
  state.eventScenes.push({
    type,
    x: spot.x,
    y: spot.y,
    color,
    life: type === "raid" ? 4.8 : 4.2,
    maxLife: type === "raid" ? 4.8 : 4.2,
    phase: Math.random() * Math.PI * 2
  });
  while (state.eventScenes.length > 4) state.eventScenes.shift();
}

function convertOpponent(ownerId, cutX, cutY) {
  const opponentIndex = state.opponents.findIndex((agent) => agent.ownerId === ownerId);
  if (opponentIndex === -1) return false;

  const [opponent] = state.opponents.splice(opponentIndex, 1);
  let convertedCells = 0;
  for (let i = 0; i < state.owner.length; i += 1) {
    if (state.owner[i] === ownerId) {
      state.owner[i] = 1;
      convertedCells += 1;
    }
    if (state.trail[i] === ownerId) {
      state.trail[i] = 0;
      convertedCells += 1;
    }
  }

  const joinPoint = {
    x: typeof cutX === "number" ? cutX : opponent.x,
    y: typeof cutY === "number" ? cutY : opponent.y
  };
  const joined = clamp(5 + Math.floor(convertedCells / 18), 5, 16);
  for (let i = 0; i < joined; i += 1) {
    state.supporters.push(createSupporter(joinPoint, opponent.color));
  }
  while (state.supporters.length > 42) state.supporters.shift();
  state.supportersConverted += joined;
  adjustNeta({ support: Math.ceil(joined / 2), power: 4, reputation: 2 });
  addConversionBurst(joinPoint.x, joinPoint.y, opponent.color);
  addFeed(`${opponent.name} joined your rally. +${joined} supporters.`);
  showToast(`${opponent.name} converted. Supporters joined.`);
  playSound("convert");
  triggerHaptic([10, 25, 14]);
  triggerScreenShake(3.5, 0.18);
  if (state.opponents.length === 0) {
    addFeed("All local rivals joined your rally. Keep winning booths.");
  }
  updateStats();
  return true;
}

function resetGame() {
  const activeRegion = getActiveRegion();
  generateRegionMask(activeRegion);
  state.owner.fill(0);
  state.trail.fill(0);
  const playerSpawn = findMaskedSpawn(31, 21);
  state.player = createAgent(1, state.party.name, state.party.color, state.party.symbol, playerSpawn.x, playerSpawn.y);
  state.player.speed = state.demoMode ? 7.15 : 6.4;
  state.player.dirX = 0;
  state.player.dirY = 0;
  state.opponents = [];
  state.supporters = [];
  state.ambientPeople = createAmbientPeople(activeRegion);
  state.eventScenes = [];
  state.conversionBursts = [];
  state.claimBursts = [];
  state.supportersConverted = 0;
  state.keys.clear();
  state.pointer.active = false;
  state.touchCue = null;
  state.dangerCue = null;
  state.dangerHapticClock = 0;
  state.timeLeft = state.demoMode ? 80 : ROUND_SECONDS;
  state.roundElapsed = 0;
  state.roundStarted = false;
  state.eventClock = state.demoMode ? 5 : 8;
  state.boostClock = 0;
  state.speedMul = 1;
  state.influence = 0;
  state.neta.decisionClock = 0;
  feedList.innerHTML = "";

  claimDisk(1, state.player.x, state.player.y, state.demoMode ? 4.8 : 3.8);
  const spots = [
    [11, 10],
    [52, 11],
    [51, 32]
  ];
  state.data.opponentParties.slice(0, 3).forEach((party, i) => {
    const [sx, sy] = spots[i];
    const { x, y } = findMaskedSpawn(sx, sy);
    const agent = createAgent(i + 2, party.name, party.color, party.symbol, x, y);
    state.opponents.push(agent);
    claimDisk(agent.ownerId, x, y, 3.2);
  });

  addFeed(`${activeRegion ? activeRegion.name : "District"} campaign office opened.`);
  addFeed(`Neta meter: support ${state.neta.support}, funds ${state.neta.funds}, reputation ${state.neta.reputation}.`);
  addFeed("Volunteers are waiting near the booths.");
  eventStat.textContent = "Ready";
  updateStats();
  updateNetaPanel();
}

function startGame() {
  ensureAudio();
  const name = partyNameInput.value.trim();
  const slogan = sloganInput.value.trim();
  const error = validatePartyName(name) || validateSlogan(slogan);
  nameError.textContent = error;
  if (error) return;

  state.party = {
    name,
    slogan: slogan || "Sabka meme, sabka game",
    color: colorInput.value,
    symbol: symbolInput.value
  };
  setPartyPreview();
  renderRegionHub();
  const resumableRegion = getResumableActiveRegion();
  if (resumableRegion) {
    selectRegion(resumableRegion.id);
    showToast(`${state.party.name} resumes ${resumableRegion.name}.`);
  } else {
    if (getActiveRegion()) {
      state.campaign.activeRegionId = null;
      saveCampaignProgress();
    }
    openRegionModal();
    showToast(`${state.party.name} is ready. Choose a state or UT.`);
  }
  showOnboarding(false);
}

function beginRound() {
  if (state.mode !== "playing" || state.roundStarted) return;
  state.roundStarted = true;
  state.eventClock = 8;
  eventStat.textContent = "Campaign";
  updateMobileHint();
  showToast("Campaign yatra started.");
  triggerHaptic(10);
  updateNetaPanel();
}

function setDirection(agent, dir) {
  if (!agent) return;
  if (agent === state.player) beginRound();
  if (dir === "up") {
    agent.dirX = 0;
    agent.dirY = -1;
  }
  if (dir === "down") {
    agent.dirX = 0;
    agent.dirY = 1;
  }
  if (dir === "left") {
    agent.dirX = -1;
    agent.dirY = 0;
  }
  if (dir === "right") {
    agent.dirX = 1;
    agent.dirY = 0;
  }
}

function applyKeyboardDirection() {
  if (state.keys.has("ArrowUp") || state.keys.has("w")) setDirection(state.player, "up");
  if (state.keys.has("ArrowDown") || state.keys.has("s")) setDirection(state.player, "down");
  if (state.keys.has("ArrowLeft") || state.keys.has("a")) setDirection(state.player, "left");
  if (state.keys.has("ArrowRight") || state.keys.has("d")) setDirection(state.player, "right");
}

function cellFromAgent(agent) {
  return {
    x: clamp(Math.floor(agent.x), 0, COLS - 1),
    y: clamp(Math.floor(agent.y), 0, ROWS - 1)
  };
}

function recordTrail(agent, x, y) {
  const idx = index(x, y);
  if (state.owner[idx] === agent.ownerId) {
    if (agent.trailCells.length > 2) closeLoop(agent);
    return;
  }
  if (state.trail[idx] === agent.ownerId) {
    // Self-crossing is allowed so players can draw messier, more natural routes.
    return;
  }
  if (state.trail[idx] && state.trail[idx] !== agent.ownerId) {
    const cutOwner = state.trail[idx];
    if (agent.ownerId === 1 && cutOwner > 1) {
      convertOpponent(cutOwner, x, y);
      return;
    }
    clearTrail(cutOwner);
    if (cutOwner === 1) {
      triggerScreenShake(6, 0.26);
      finishRound(false, "Opposition cut your campaign route.");
    }
    return;
  }
  state.trail[idx] = agent.ownerId;
  agent.trailCells.push(idx);
  agent.trailPoints.push({ x: agent.x, y: agent.y });
  if (agent.trailPoints.length > 180) agent.trailPoints.shift();
}

function clearTrail(ownerId) {
  for (let i = 0; i < state.trail.length; i += 1) {
    if (state.trail[i] === ownerId) state.trail[i] = 0;
  }
  const agent = ownerId === 1 ? state.player : state.opponents.find((item) => item.ownerId === ownerId);
  if (agent) {
    agent.trailCells = [];
    agent.trailPoints = [];
  }
}

function closeLoop(agent) {
  const claimedCells = [...agent.trailCells];
  for (const idx of agent.trailCells) {
    state.owner[idx] = agent.ownerId;
    state.trail[idx] = 0;
  }

  const visited = new Uint8Array(COLS * ROWS);
  const queue = [];
  const push = (x, y) => {
    const idx = index(x, y);
    if (!isMaskedCell(x, y) || visited[idx] || state.owner[idx] === agent.ownerId) return;
    visited[idx] = 1;
    queue.push([x, y]);
  };

  for (let x = 0; x < COLS; x += 1) {
    push(x, 0);
    push(x, ROWS - 1);
  }
  for (let y = 0; y < ROWS; y += 1) {
    push(0, y);
    push(COLS - 1, y);
  }

  for (let i = 0; i < queue.length; i += 1) {
    const [x, y] = queue[i];
    if (x > 0) push(x - 1, y);
    if (x < COLS - 1) push(x + 1, y);
    if (y > 0) push(x, y - 1);
    if (y < ROWS - 1) push(x, y + 1);
  }

  let gained = 0;
  for (let i = 0; i < state.owner.length; i += 1) {
    if (state.regionMask[i] && !visited[i] && state.owner[i] !== agent.ownerId) {
      state.owner[i] = agent.ownerId;
      state.trail[i] = 0;
      claimedCells.push(i);
      gained += 1;
    }
  }

  if (agent.ownerId === 1 && agent.trailCells.length + gained > 5) {
    const boothGain = agent.trailCells.length + gained;
    const supportGain = clamp(Math.floor(boothGain / (state.demoMode ? 13 : 18)) + (state.demoMode ? 1 : 0), 1, state.demoMode ? 10 : 8);
    const fundsGain = clamp(Math.floor(boothGain / (state.demoMode ? 18 : 24)), 0, state.demoMode ? 9 : 7);
    const powerGain = boothGain > (state.demoMode ? 26 : 34) ? 2 : 1;
    adjustNeta({ support: supportGain, funds: fundsGain, power: powerGain }, false);
    addFeed(`${state.party.name} won ${boothGain} new booths. Support +${supportGain}.`);
    showToast("Campaign loop closed. Influence gained.");
    addClaimBurst(claimedCells, state.party.color);
    playSound("loop");
    triggerHaptic([8, 24, 16]);
    triggerScreenShake(2, 0.14);
    saveCampaignProgress();
  }
  agent.trailCells = [];
  agent.trailPoints = [];
}

function updateAgent(agent, dt) {
  const speed = agent.ownerId === 1 ? agent.speed * state.speedMul : agent.speed;
  const nextX = clamp(agent.x + agent.dirX * speed * dt, 1, COLS - 1.01);
  const nextY = clamp(agent.y + agent.dirY * speed * dt, 1, ROWS - 1.01);
  if (isMaskedCell(nextX, nextY)) {
    agent.x = nextX;
    agent.y = nextY;
  } else if (agent.ownerId !== 1) {
    agent.dirX = -agent.dirX || (Math.random() > 0.5 ? 1 : -1);
    agent.dirY = -agent.dirY;
    agent.turnClock = 0;
  }
  const { x, y } = cellFromAgent(agent);
  recordTrail(agent, x, y);
}

function updateOpponents(dt) {
  for (const agent of state.opponents) {
    agent.turnClock -= dt;
    const { x, y } = cellFromAgent(agent);
    const nearWall = x <= 2 || x >= COLS - 3 || y <= 2 || y >= ROWS - 3;
    if (agent.turnClock <= 0 || nearWall || Math.random() < 0.01) {
      let target = null;
      if (state.player?.trailPoints?.length > 3 && Math.random() < 0.55) {
        target = state.player.trailPoints[Math.floor(Math.random() * state.player.trailPoints.length)];
      } else if (Math.random() < 0.35) {
        target = { x: state.player.x, y: state.player.y };
      } else {
        target = randomMaskedPoint();
      }
      agent.targetX = target.x;
      agent.targetY = target.y;
      const dx = agent.targetX - agent.x;
      const dy = agent.targetY - agent.y;
      if (Math.abs(dx) > Math.abs(dy)) {
        agent.dirX = Math.sign(dx) || (Math.random() > 0.5 ? 1 : -1);
        agent.dirY = 0;
      } else {
        agent.dirX = 0;
        agent.dirY = Math.sign(dy) || (Math.random() > 0.5 ? 1 : -1);
      }
      agent.turnClock = 0.55 + Math.random() * 0.95;
    }
    updateAgent(agent, dt);
  }
}

function handleTrailCuts() {
  if (!state.player) return;
  const playerCell = cellFromAgent(state.player);
  const playerIdx = index(playerCell.x, playerCell.y);
  if (state.trail[playerIdx] > 1) {
    convertOpponent(state.trail[playerIdx], playerCell.x, playerCell.y);
  }

  for (const agent of [...state.opponents]) {
    const distanceToPlayer = Math.hypot(agent.x - state.player.x, agent.y - state.player.y);
    if (distanceToPlayer < 1.15) {
      convertOpponent(agent.ownerId, agent.x, agent.y);
      continue;
    }
    const cell = cellFromAgent(agent);
    const idx = index(cell.x, cell.y);
    if (state.trail[idx] === 1) {
      triggerScreenShake(6, 0.26);
      finishRound(false, `${agent.name} cut your rally route.`);
      return;
    }
  }
}

function updateRouteDanger(dt) {
  state.dangerHapticClock = Math.max(0, state.dangerHapticClock - dt);
  if (!state.player || state.player.trailPoints.length < 4 || state.opponents.length === 0) {
    if (state.dangerCue) {
      state.dangerCue.life -= dt;
      if (state.dangerCue.life <= 0) state.dangerCue = null;
    }
    return;
  }

  let best = null;
  const trail = state.player.trailPoints.slice(-72);
  for (const opponent of state.opponents) {
    for (let i = trail.length - 1; i >= 0; i -= 3) {
      const point = trail[i];
      const distance = Math.hypot(opponent.x - point.x, opponent.y - point.y);
      if (!best || distance < best.distance) {
        best = { distance, x: point.x, y: point.y, color: opponent.color, name: opponent.name };
      }
    }
  }

  if (best && best.distance < 4.25) {
    state.dangerCue = {
      x: best.x,
      y: best.y,
      color: best.color,
      level: clamp(1 - best.distance / 4.25, 0.18, 1),
      life: 0.34,
      maxLife: 0.34
    };
    if (best.distance < 2.65 && state.dangerHapticClock <= 0) {
      triggerHaptic([6, 18, 10]);
      state.dangerHapticClock = 1.4;
    }
  } else if (state.dangerCue) {
    state.dangerCue.life -= dt;
    if (state.dangerCue.life <= 0) state.dangerCue = null;
  }
}

function triggerEvent() {
  const event = state.data.randomEvents[Math.floor(Math.random() * state.data.randomEvents.length)];
  const soundForEffect = {
    dholBoost: "dhol",
    memeWave: "meme",
    posterRain: "poster",
    raid: "lose",
    teaBreak: "tap",
    speedUp: "rally"
  };
  eventStat.textContent = event.title;
  addFeed(event.title);
  showToast(event.copy);
  playSound(soundForEffect[event.effect] || "event");

  if (event.effect === "speedUp") {
    state.speedMul = 1.28;
    state.boostClock = 5;
  } else if (event.effect === "speedDown") {
    state.speedMul = 0.82;
    state.boostClock = 5;
  } else if (event.effect === "raid") {
    state.speedMul = 0.74;
    state.boostClock = 4.2;
    addEventScene("raid", state.player.x, state.player.y, "#151515");
    addConversionBurst(state.player.x, state.player.y, "#151515");
  } else if (event.effect === "teaBreak") {
    state.speedMul = 0.88;
    state.boostClock = 3.2;
    addEventScene("teaBreak", state.player.x, state.player.y, state.party.color);
    const cells = claimDisk(1, state.player.x, state.player.y, 2.4);
    addClaimBurst(cells, state.party.color);
  } else if (event.effect === "claimBurst") {
    const cells = claimDisk(1, state.player.x, state.player.y, 3.8);
    addClaimBurst(cells, state.party.color);
  } else if (event.effect === "memeWave") {
    const cells = claimDisk(1, state.player.x, state.player.y, 3.2);
    addClaimBurst(cells, state.party.color);
    addConversionBurst(state.player.x, state.player.y, state.party.color);
    addEventScene("memeWave", state.player.x, state.player.y, state.party.color);
    triggerScreenShake(3, 0.18);
  } else if (event.effect === "dholBoost") {
    state.speedMul = 1.35;
    state.boostClock = 5.4;
    addEventScene("dholBoost", state.player.x, state.player.y, "#ffd166");
    addConversionBurst(state.player.x, state.player.y, "#ffd166");
  } else if (event.effect === "posterRain") {
    const cells = claimDisk(1, state.player.x, state.player.y, 4.6);
    addClaimBurst(cells, state.party.color);
    addEventScene("posterRain", state.player.x, state.player.y, state.party.color);
    triggerScreenShake(2, 0.14);
  } else if (event.effect === "claimLine") {
    for (const idx of state.player.trailCells.slice(-16)) {
      state.owner[idx] = 1;
      state.trail[idx] = 0;
    }
    state.player.trailCells = [];
    state.player.trailPoints = [];
  }
  if (event.impact) {
    adjustNeta(event.impact);
  }
}

function updateSupporters(dt) {
  if (!state.player || state.supporters.length === 0) return;
  const sideX = -state.player.dirY;
  const sideY = state.player.dirX;
  const backX = -state.player.dirX;
  const backY = -state.player.dirY;

  state.supporters.forEach((supporter, i) => {
    supporter.phase += dt * (1.5 + supporter.size * 2);
    const looseBack = 1.1 + supporter.spread + Math.sin(supporter.phase) * 0.38;
    const looseSide = supporter.driftX + Math.cos(supporter.phase * 0.9) * (0.72 + supporter.spread * 0.16);
    const targetX = state.player.x + backX * looseBack + sideX * looseSide + Math.sin(supporter.phase * 1.7) * 0.22;
    const targetY = state.player.y + backY * looseBack + sideY * looseSide + supporter.driftY * 0.35 + Math.cos(supporter.phase * 1.3) * 0.22;
    const dx = targetX - supporter.x;
    const dy = targetY - supporter.y;
    const distance = Math.hypot(dx, dy) || 1;
    const step = Math.min(distance, supporter.followSpeed * dt);
    supporter.x = clamp(supporter.x + (dx / distance) * step, 1, COLS - 1.01);
    supporter.y = clamp(supporter.y + (dy / distance) * step, 1, ROWS - 1.01);
  });

  for (let i = 0; i < state.supporters.length; i += 1) {
    for (let j = i + 1; j < state.supporters.length; j += 1) {
      const a = state.supporters[i];
      const b = state.supporters[j];
      const dx = b.x - a.x;
      const dy = b.y - a.y;
      const distance = Math.hypot(dx, dy) || 1;
      if (distance < 0.72) {
        const push = (0.72 - distance) * 0.22;
        const nx = dx / distance;
        const ny = dy / distance;
        a.x = clamp(a.x - nx * push, 1, COLS - 1.01);
        a.y = clamp(a.y - ny * push, 1, ROWS - 1.01);
        b.x = clamp(b.x + nx * push, 1, COLS - 1.01);
        b.y = clamp(b.y + ny * push, 1, ROWS - 1.01);
      }
    }
  }
}

function updateConversionBursts(dt) {
  if (state.conversionBursts.length === 0) return;
  for (const particle of state.conversionBursts) {
    particle.life -= dt;
    particle.x += particle.vx * dt;
    particle.y += particle.vy * dt;
    particle.vx *= 0.96;
    particle.vy *= 0.96;
  }
  state.conversionBursts = state.conversionBursts.filter((particle) => particle.life > 0);
}

function updateClaimBursts(dt) {
  if (state.claimBursts.length === 0) return;
  for (const burst of state.claimBursts) {
    if (burst.delay > 0) {
      burst.delay -= dt;
      continue;
    }
    burst.life -= dt;
  }
  state.claimBursts = state.claimBursts.filter((burst) => burst.delay > 0 || burst.life > 0);
}

function updateEventScenes(dt) {
  if (state.eventScenes.length === 0) return;
  for (const scene of state.eventScenes) {
    scene.life -= dt;
    scene.phase += dt * 3.2;
  }
  state.eventScenes = state.eventScenes.filter((scene) => scene.life > 0);
}

function updateStats() {
  let playerCells = 0;
  let playableCells = 0;
  for (let i = 0; i < state.owner.length; i += 1) {
    if (!state.regionMask[i]) continue;
    playableCells += 1;
    if (state.owner[i] === 1) playerCells += 1;
  }
  state.influence = Math.round((playerCells / Math.max(1, playableCells)) * 100);
  const activeRegion = getActiveRegion();
  regionStat.textContent = activeRegion ? activeRegion.name : "Choose State";
  influenceStat.textContent = `${state.influence}%`;
  timeStat.textContent = `${Math.ceil(state.timeLeft)}s`;
  updateNetaPanel();
}

function finishRound(won, reason) {
  if (state.mode !== "playing") return;
  state.mode = "result";
  updateMobileHint();
  clearTrail(1);
  const finalScore = mandateScore();
  const victory = Boolean(won);
  const activeRegion = getActiveRegion();
  const wonRegion = victory ? markActiveRegionWon() : null;
  const nationalComplete = victory && state.campaign.nationalWon;
  const regionName = (wonRegion || activeRegion)?.name || "district";
  const supporterLine = state.supportersConverted
    ? ` ${state.supportersConverted} converted supporters joined the yatra.`
    : " The poster team is already posing for the reel.";
  const headline = victory
    ? nationalComplete
      ? `${state.party.name} wins national mandate`
      : `${state.party.name} wins ${regionName} mandate`
    : `${state.party.name} gets a ${regionName} recount`;
  const copy = victory
    ? nationalComplete
      ? `${REGIONS.length}/${REGIONS.length} regions won. National leader stands with folded hands, and the world yatra board is open.${supporterLine}`
      : `${state.influence}% influence, ${state.neta.support} support, mandate score ${finalScore} in ${regionName}.${supporterLine}`
    : `${reason} Final mandate score: ${finalScore} in ${regionName}. Support ${state.neta.support}, reputation ${state.neta.reputation}. New strategy meeting starts now.`;
  state.shareText = nationalComplete
    ? `NETA JI: ${state.party.name} completed the fictional national mandate. ${state.party.slogan}`
    : `NETA JI: ${state.party.name} scored ${finalScore} fictional comedy mandate in ${regionName}. ${state.party.slogan}`;
  state.resultSummary = {
    headline,
    copy,
    stamp: victory ? (nationalComplete ? "National" : "Won") : "Recount",
    victory,
    nationalComplete,
    regionName,
    score: finalScore,
    influence: state.influence,
    support: state.neta.support,
    reputation: state.neta.reputation,
    completedRegions: completedRegionCount(),
    partyName: state.party.name,
    slogan: state.party.slogan,
    color: state.party.color,
    symbol: symbolLabel(state.party.symbol)
  };
  resultHeadline.textContent = headline;
  resultCopy.textContent = copy;
  resultStamp.textContent = victory ? (nationalComplete ? "National" : "Won") : "Recount";
  resultSymbol.textContent = symbolLabel(state.party.symbol);
  resultModal.style.setProperty("--party-color", state.party.color);
  resultModal.dataset.result = victory ? (nationalComplete ? "national" : "win") : "loss";
  eventStat.textContent = victory ? "Mandate Won" : "Recount";
  nextRegionBtn.hidden = !victory;
  nextRegionBtn.textContent = nationalComplete ? "India Map" : "Next State";
  if (victory) {
    addFeed(nationalComplete ? "National mandate complete. World yatra teaser unlocked." : `${regionName} mandate won. Red flag raised on the India map.`);
    playSound("win");
    triggerHaptic(nationalComplete ? [20, 40, 20, 40, 36] : [14, 30, 22]);
    resultModal.classList.add("is-open");
    showToast(nationalComplete ? "National mandate complete." : `${regionName} won. Red flag ready on the India map.`);
    return;
  }
  playSound("lose");
  triggerHaptic([24, 60, 16]);
  resultModal.classList.add("is-open");
}

function update(dt) {
  if (state.mode !== "playing") return;
  updateTouchCue(dt);
  if (state.paused) {
    state.toastClock -= dt;
    if (state.toastClock <= 0) toast.classList.remove("is-visible");
    updateNetaPanel();
    updateStats();
    return;
  }
  applyKeyboardDirection();
  if (!state.roundStarted) {
    updateAmbientPeople(dt * 0.35);
    state.toastClock -= dt;
    if (state.toastClock <= 0) toast.classList.remove("is-visible");
    updateNetaPanel();
    updateStats();
    return;
  }
  state.timeLeft -= dt;
  state.roundElapsed += dt;
  state.eventClock -= dt;
  state.toastClock -= dt;
  if (state.toastClock <= 0) toast.classList.remove("is-visible");

  if (state.boostClock > 0) {
    state.boostClock -= dt;
    if (state.boostClock <= 0) state.speedMul = 1;
  }
  if (state.neta.decisionClock > 0) {
    state.neta.decisionClock = Math.max(0, state.neta.decisionClock - dt);
  }

  updateAmbientPeople(dt);
  updateAgent(state.player, dt);
  updateOpponents(dt);
  updateSupporters(dt);
  updateConversionBursts(dt);
  updateClaimBursts(dt);
  updateEventScenes(dt);
  updateRouteDanger(dt);
  handleTrailCuts();

  if (state.eventClock <= 0) {
    triggerEvent();
    state.eventClock = 12 + Math.random() * 8;
  }

  updateStats();
  const score = mandateScore();
  const earlyEnough = state.roundElapsed >= (state.demoMode ? 7 : MIN_WIN_SECONDS);
  const influenceTarget = state.demoMode ? 44 : 52;
  const scoreTarget = state.demoMode ? 50 : 55;
  if (earlyEnough && state.influence >= influenceTarget && score >= scoreTarget) {
    finishRound(true, "District mandate reached.");
  } else if (state.timeLeft <= 0) {
    finishRound(state.influence >= 30 && score >= 44, "The campaign clock ended.");
  }
}

function ownerColor(ownerId) {
  if (ownerId === 1) return state.party.color;
  const opponent = state.opponents.find((agent) => agent.ownerId === ownerId);
  return opponent ? opponent.color : "#cbc2b3";
}

function shadeHex(hex, amount) {
  const value = hex.replace("#", "");
  if (value.length !== 6) return hex;
  const next = [0, 2, 4]
    .map((start) => clamp(parseInt(value.slice(start, start + 2), 16) + amount, 0, 255).toString(16).padStart(2, "0"))
    .join("");
  return "#" + next;
}

function getRegionMapPoint(region, rect = state.mapRect) {
  const polygons = getRegionMapPolygons(region);
  if (polygons) {
    const point = getLargestPolygonCentroid(polygons);
    return {
      x: rect.x + point.x * rect.width,
      y: rect.y + point.y * rect.height
    };
  }
  const position = REGION_POSITIONS[region.id] || [0.5, 0.5];
  return {
    x: rect.x + position[0] * rect.width,
    y: rect.y + position[1] * rect.height
  };
}

function getRegionMapPolygons(region) {
  return REGION_MAP_SHAPES[region?.id] || null;
}

function getPolygonArea(points) {
  let area = 0;
  for (let i = 0; i < points.length; i += 1) {
    const current = points[i];
    const next = points[(i + 1) % points.length];
    area += current[0] * next[1] - next[0] * current[1];
  }
  return Math.abs(area / 2);
}

function getPolygonCentroid(points) {
  let areaTwice = 0;
  let cx = 0;
  let cy = 0;
  for (let i = 0; i < points.length; i += 1) {
    const current = points[i];
    const next = points[(i + 1) % points.length];
    const cross = current[0] * next[1] - next[0] * current[1];
    areaTwice += cross;
    cx += (current[0] + next[0]) * cross;
    cy += (current[1] + next[1]) * cross;
  }
  if (Math.abs(areaTwice) < 0.0001) {
    return points.reduce((sum, point) => ({ x: sum.x + point[0] / points.length, y: sum.y + point[1] / points.length }), { x: 0, y: 0 });
  }
  return {
    x: cx / (3 * areaTwice),
    y: cy / (3 * areaTwice)
  };
}

function getLargestPolygonCentroid(polygons) {
  const largest = polygons.reduce((best, polygon) => (getPolygonArea(polygon) > getPolygonArea(best) ? polygon : best), polygons[0]);
  return getPolygonCentroid(largest);
}

function normalizedMapPoint(point, rect = state.mapRect) {
  return {
    x: (point.x - rect.x) / rect.width,
    y: (point.y - rect.y) / rect.height
  };
}

function pointInNormalizedPolygon(point, polygon) {
  let inside = false;
  for (let i = 0, j = polygon.length - 1; i < polygon.length; j = i, i += 1) {
    const xi = polygon[i][0];
    const yi = polygon[i][1];
    const xj = polygon[j][0];
    const yj = polygon[j][1];
    if ((yi > point.y) !== (yj > point.y) && point.x < ((xj - xi) * (point.y - yi)) / (yj - yi) + xi) {
      inside = !inside;
    }
  }
  return inside;
}

function pointInRegionMapShape(point, region) {
  const polygons = getRegionMapPolygons(region);
  if (!polygons) return false;
  const normalized = normalizedMapPoint(point);
  return polygons.some((polygon) => pointInNormalizedPolygon(normalized, polygon));
}

function getContainingRegionPolygonArea(point, region) {
  const polygons = getRegionMapPolygons(region);
  if (!polygons) return Infinity;
  const normalized = normalizedMapPoint(point);
  const containingAreas = polygons
    .filter((polygon) => pointInNormalizedPolygon(normalized, polygon))
    .map(getPolygonArea);
  return containingAreas.length ? Math.min(...containingAreas) : Infinity;
}

function drawRegionMapPolygons(region, fill, options = {}) {
  const polygons = getRegionMapPolygons(region);
  if (!polygons) return false;
  const rect = state.mapRect;
  const point = getRegionMapPoint(region, rect);
  const profile = getRegionShapeProfile(region);

  ctx.save();
  ctx.fillStyle = fill;
  ctx.strokeStyle = "#151515";
  ctx.lineWidth = options.pending ? Math.max(2.8, rect.width * 0.007) : Math.max(1.8, rect.width * 0.0048);
  ctx.lineJoin = "round";
  ctx.lineCap = "round";

  for (const polygon of polygons) {
    ctx.beginPath();
    polygon.forEach(([px, py], indexValue) => {
      const x = rect.x + px * rect.width;
      const y = rect.y + py * rect.height;
      if (indexValue === 0) ctx.moveTo(x, y);
      else ctx.lineTo(x, y);
    });
    ctx.closePath();
    ctx.fill();
    ctx.stroke();
  }

  const canLabel = rect.width > 285 && (region.type !== "UT" || options.pending || state.campaign.completed[region.id]);
  if (profile.label && canLabel && !options.noLabel) {
    const isDarkFill = fill !== "#fffdf7" && fill !== "#ffd166";
    ctx.fillStyle = isDarkFill ? "#fffdf7" : "rgba(21, 21, 21, 0.8)";
    ctx.font = `900 ${Math.max(6.5, rect.width * (region.type === "UT" ? 0.013 : 0.016))}px ui-sans-serif`;
    ctx.textAlign = "center";
    ctx.textBaseline = "middle";
    ctx.fillText(profile.label, point.x, point.y, Math.max(20, rect.width * 0.11));
  }
  ctx.restore();
  return true;
}

function getRegionBlobSize(region) {
  return (
    REGION_BLOB_SIZE[region.id] ||
    (region.type === "UT" ? REGION_BLOB_SIZE.defaultUt : REGION_BLOB_SIZE.defaultState)
  );
}

function drawNormalizedPath(points, rect) {
  ctx.beginPath();
  points.forEach(([px, py], indexValue) => {
    const x = rect.x + px * rect.width;
    const y = rect.y + py * rect.height;
    if (indexValue === 0) ctx.moveTo(x, y);
    else ctx.lineTo(x, y);
  });
  ctx.closePath();
}

function drawCartoonIndiaBase(rect) {
  ctx.save();
  ctx.shadowColor = "rgba(21, 21, 21, 0.2)";
  ctx.shadowBlur = 0;
  ctx.shadowOffsetX = 5;
  ctx.shadowOffsetY = 6;
  ctx.fillStyle = "#f6e7bf";
  ctx.strokeStyle = "#151515";
  ctx.lineWidth = Math.max(4, rect.width * 0.014);
  ctx.lineJoin = "round";
  drawNormalizedPath(INDIA_OUTLINE, rect);
  ctx.fill();
  ctx.stroke();
  drawNormalizedPath(NORTH_EAST_OUTLINE, rect);
  ctx.fill();
  ctx.stroke();
  ctx.shadowColor = "transparent";
  ctx.lineWidth = Math.max(3, rect.width * 0.01);
  ctx.strokeStyle = "#151515";
  ctx.beginPath();
  ctx.moveTo(rect.x + rect.width * 0.64, rect.y + rect.height * 0.38);
  ctx.quadraticCurveTo(
    rect.x + rect.width * 0.72,
    rect.y + rect.height * 0.33,
    rect.x + rect.width * 0.79,
    rect.y + rect.height * 0.37
  );
  ctx.stroke();

  const islands = [
    [0.24, 0.83, 3.8],
    [0.23, 0.87, 3],
    [0.25, 0.9, 2.8],
    [0.78, 0.79, 3.4],
    [0.79, 0.84, 3.2],
    [0.8, 0.9, 3],
    [0.81, 0.95, 2.7]
  ];
  ctx.fillStyle = "#f6e7bf";
  for (const [px, py, radius] of islands) {
    ctx.beginPath();
    ctx.arc(rect.x + px * rect.width, rect.y + py * rect.height, radius, 0, Math.PI * 2);
    ctx.fill();
    ctx.stroke();
  }
  ctx.restore();
}

function drawRegionBlob(region, fill, options = {}) {
  if (drawRegionMapPolygons(region, fill, options)) return;

  const rect = state.mapRect;
  const point = getRegionMapPoint(region, rect);
  const [rxNorm, ryNorm] = getRegionBlobSize(region);
  const rx = rect.width * rxNorm;
  const ry = rect.height * ryNorm;
  const profile = getRegionShapeProfile(region);
  const seed = hashText(region.id);

  ctx.save();
  ctx.fillStyle = fill;
  ctx.strokeStyle = "#151515";
  ctx.lineWidth = options.pending ? Math.max(2.6, rect.width * 0.006) : Math.max(1.4, rect.width * 0.0038);
  ctx.lineJoin = "round";
  ctx.lineCap = "round";

  if (region.id === "andaman-nicobar" || region.id === "lakshadweep") {
    const dots = region.id === "andaman-nicobar" ? 5 : 4;
    for (let i = 0; i < dots; i += 1) {
      const y = point.y - ry * 0.8 + i * (ry * 0.42);
      const x = point.x + Math.sin(i * 1.7 + seed) * rx * 0.35;
      ctx.beginPath();
      ctx.arc(x, y, Math.max(3.2, rx * (0.28 - i * 0.01)), 0, Math.PI * 2);
      ctx.fill();
      ctx.stroke();
    }
    ctx.restore();
    return;
  }

  const localPoints = getRegionShapeLocalPoints(region, 0.04);
  ctx.beginPath();
  localPoints.forEach(([localX, localY], i) => {
    const p = transformRegionShapePoint(localX, localY, point.x, point.y, rx, ry, profile);
    if (i === 0) ctx.moveTo(p.x, p.y);
    else ctx.lineTo(p.x, p.y);
  });
  ctx.closePath();
  ctx.fill();
  ctx.stroke();

  const canLabel = rect.width > 270 && (region.type !== "UT" || options.pending || state.campaign.completed[region.id]);
  if (profile.label && canLabel && !options.noLabel) {
    const isDarkFill = fill !== "#fffdf7" && fill !== "#ffd166";
    ctx.fillStyle = isDarkFill ? "#fffdf7" : "rgba(21, 21, 21, 0.78)";
    ctx.font = `900 ${Math.max(6.5, rect.width * (region.type === "UT" ? 0.014 : 0.017))}px ui-sans-serif`;
    ctx.textAlign = "center";
    ctx.textBaseline = "middle";
    ctx.fillText(profile.label, point.x, point.y, rx * 1.7);
  }
  ctx.restore();
}

function drawFlag(x, y, color, scale = 1) {
  ctx.save();
  ctx.lineWidth = 2 * scale;
  ctx.strokeStyle = "#151515";
  ctx.fillStyle = "#151515";
  ctx.beginPath();
  ctx.moveTo(x, y - 12 * scale);
  ctx.lineTo(x, y + 15 * scale);
  ctx.stroke();
  ctx.fillStyle = color;
  ctx.beginPath();
  ctx.moveTo(x + 1 * scale, y - 12 * scale);
  ctx.lineTo(x + 20 * scale, y - 8 * scale);
  ctx.lineTo(x + 2 * scale, y - 2 * scale);
  ctx.closePath();
  ctx.fill();
  ctx.stroke();
  ctx.fillStyle = "#fffdf7";
  ctx.beginPath();
  ctx.arc(x, y + 16 * scale, 4.5 * scale, 0, Math.PI * 2);
  ctx.fill();
  ctx.stroke();
  ctx.restore();
}

function drawMapLegend(width, height) {
  const legendW = Math.min(width - 24, 244);
  const legendX = 12;
  const legendY = 12;
  const items = [
    ["#151515", "New"],
    ["#ffd166", "Selected"],
    ["#d92d20", "Won"]
  ];

  ctx.save();
  ctx.fillStyle = "rgba(255, 253, 247, 0.92)";
  ctx.strokeStyle = "#151515";
  ctx.lineWidth = 1.5;
  ctx.beginPath();
  ctx.roundRect(legendX, legendY, legendW, width < 430 ? 30 : 34, 8);
  ctx.fill();
  ctx.stroke();
  ctx.font = `900 ${width < 430 ? 9 : 10}px ui-sans-serif`;
  ctx.textAlign = "left";
  ctx.textBaseline = "middle";
  items.forEach(([color, label], i) => {
    const x = legendX + 12 + i * (legendW / 3);
    ctx.fillStyle = color;
    ctx.beginPath();
    ctx.roundRect(x, legendY + (width < 430 ? 9 : 10), 14, 10, 2);
    ctx.fill();
    ctx.stroke();
    ctx.fillStyle = "#151515";
    ctx.fillText(label, x + 19, legendY + (width < 430 ? 15 : 17), legendW / 3 - 24);
  });
  ctx.restore();
}

function drawSelectedRegionCard(region, width, height) {
  if (!region) return;
  const point = getRegionMapPoint(region);
  const isWon = Boolean(state.campaign.completed[region.id]);
  const cardW = Math.min(width - 28, width < 430 ? 210 : 250);
  const cardH = width < 430 ? 54 : 60;
  const cardX = clamp(point.x - cardW / 2, 14, width - cardW - 14);
  const cardY = clamp(point.y - cardH - 34, state.mapRect.y + 8, height - cardH - 78);

  ctx.save();
  ctx.fillStyle = "rgba(255, 253, 247, 0.96)";
  ctx.strokeStyle = "#151515";
  ctx.lineWidth = 2;
  ctx.beginPath();
  ctx.roundRect(cardX, cardY, cardW, cardH, 10);
  ctx.fill();
  ctx.stroke();

  ctx.fillStyle = isWon ? "#d92d20" : "#ffd166";
  ctx.beginPath();
  ctx.roundRect(cardX + 10, cardY + 13, 26, 26, 5);
  ctx.fill();
  ctx.stroke();

  ctx.fillStyle = "#151515";
  ctx.font = `900 ${width < 430 ? 12 : 14}px ui-sans-serif`;
  ctx.textAlign = "left";
  ctx.textBaseline = "middle";
  ctx.fillText(region.name, cardX + 44, cardY + 21, cardW - 54);
  ctx.font = `800 ${width < 430 ? 9 : 10}px ui-sans-serif`;
  ctx.fillStyle = "#625a4e";
  ctx.fillText(isWon ? "Won - replay available" : "Selected - press OK", cardX + 44, cardY + 39, cardW - 54);

  ctx.strokeStyle = "#151515";
  ctx.setLineDash([5, 4]);
  ctx.beginPath();
  ctx.moveTo(cardX + cardW / 2, cardY + cardH);
  ctx.lineTo(point.x + 7, point.y - 7);
  ctx.stroke();
  ctx.restore();
}

function drawWonPatch(region, rect) {
  const previousRect = state.mapRect;
  state.mapRect = rect;
  drawRegionBlob(region, state.party.color);
  state.mapRect = previousRect;
}

function drawMapYatra() {
  const lastRegion = REGIONS.find((region) => region.id === state.campaign.lastWonRegionId);
  const pendingRegion = REGIONS.find((region) => region.id === state.campaign.pendingRegionId || region.id === state.campaign.activeRegionId);
  if (!lastRegion && !pendingRegion) return;
  const from = lastRegion ? getRegionMapPoint(lastRegion) : getRegionMapPoint(pendingRegion);
  const to = pendingRegion ? getRegionMapPoint(pendingRegion) : from;
  const t = (Math.sin((state.lastTime || 0) * 0.0016) + 1) / 2;
  const x = from.x + (to.x - from.x) * t;
  const y = from.y + (to.y - from.y) * t;

  ctx.save();
  ctx.setLineDash([6, 5]);
  ctx.strokeStyle = "rgba(21, 21, 21, 0.55)";
  ctx.lineWidth = 2;
  ctx.beginPath();
  ctx.moveTo(from.x, from.y);
  ctx.lineTo(to.x, to.y);
  ctx.stroke();
  ctx.setLineDash([]);
  drawMiniPerson(x, y, 15, {
    color: state.party.color,
    accent: "#fffdf7",
    foldedHands: true,
    phase: 0.4
  });
  drawMiniPerson(x - 15, y + 7, 11, {
    color: state.party.color,
    accent: "#fffdf7",
    flag: true,
    phase: 1.2
  });
  drawMiniPerson(x + 15, y + 8, 11, {
    color: state.party.color,
    accent: "#fffdf7",
    phase: 2.4
  });
  ctx.restore();
}

function drawNationalCompletion(width) {
  if (!state.campaign.nationalWon) return;
  const rect = state.mapRect;
  const centerX = rect.x + rect.width * 0.5;
  const y = rect.y + rect.height * 0.5;

  ctx.save();
  ctx.fillStyle = "rgba(21, 21, 21, 0.86)";
  ctx.strokeStyle = "#fffdf7";
  ctx.lineWidth = 2;
  ctx.beginPath();
  ctx.roundRect(Math.max(12, centerX - 150), Math.max(12, y - 58), Math.min(width - 24, 300), 92, 12);
  ctx.fill();
  ctx.stroke();

  drawMiniPerson(centerX - 98, y - 12, 28, {
    color: "#fffdf7",
    accent: state.party.color,
    foldedHands: true,
    symbol: state.party.symbol,
    phase: 0.2
  });
  drawTinyFlag(centerX + 104, y - 18, 13, state.party.color);

  ctx.fillStyle = "#fffdf7";
  ctx.font = "900 15px ui-sans-serif";
  ctx.textAlign = "center";
  ctx.fillText("National Mandate Complete", centerX, y - 30, 190);
  ctx.font = "800 11px ui-sans-serif";
  ctx.fillText("World yatra teaser unlocked", centerX, y - 9, 190);
  ctx.fillText(`${REGIONS.length}/${REGIONS.length} regions won`, centerX, y + 12, 190);
  ctx.restore();
}

function drawMapHome(width, height) {
  ctx.fillStyle = "#bfe7ff";
  ctx.fillRect(0, 0, width, height);
  ctx.fillStyle = "rgba(255, 255, 255, 0.22)";
  for (let i = -height; i < width; i += 34) {
    ctx.fillRect(i, 0, 10, height);
  }

  const availableH = Math.max(240, height - 86);
  const availableW = Math.max(240, width - (width < 430 ? 56 : 44));
  const mapH = Math.min(availableH, availableW / MAP_ASPECT);
  const mapW = mapH * MAP_ASPECT;
  const x = (width - mapW) / 2;
  const y = Math.max(10, (height - mapH) / 2 - 18);
  state.mapRect = { x, y, width: mapW, height: mapH };

  ctx.save();
  ctx.fillStyle = "#7fcf94";
  ctx.beginPath();
  ctx.roundRect(x - 14, y - 14, mapW + 28, mapH + 28, 18);
  ctx.fill();
  drawCartoonIndiaBase(state.mapRect);
  const selectedRegion = REGIONS.find((region) => region.id === state.campaign.pendingRegionId);
  if (selectedRegion) {
    const pulse = 1 + Math.sin((state.lastTime || 0) * 0.006) * 0.08;
    ctx.save();
    ctx.globalAlpha = 0.76;
    ctx.shadowColor = "rgba(255, 209, 102, 0.72)";
    ctx.shadowBlur = 22 * pulse;
    drawRegionBlob(selectedRegion, "rgba(255, 209, 102, 0.58)", { pending: true, noLabel: true });
    ctx.restore();
  }

  for (const region of REGIONS) {
    const won = Boolean(state.campaign.completed[region.id]);
    const pending = state.campaign.pendingRegionId === region.id;
    const active = state.campaign.activeRegionId === region.id;
    const fill = won ? state.party.color : pending ? "#ffd166" : active ? "#13a99a" : "#fffdf7";
    drawRegionBlob(region, fill, { pending });
  }

  drawMapYatra();

  const mapPulse = 1 + Math.sin((state.lastTime || 0) * 0.006) * 0.1;
  for (const region of REGIONS) {
    const point = getRegionMapPoint(region);
    const won = Boolean(state.campaign.completed[region.id]);
    const pending = state.campaign.pendingRegionId === region.id;
    const active = state.campaign.activeRegionId === region.id;
    if (pending) {
      ctx.fillStyle = "rgba(244, 197, 66, 0.5)";
      ctx.beginPath();
      ctx.arc(point.x + 8, point.y + 3, 20 * mapPulse, 0, Math.PI * 2);
      ctx.fill();
      ctx.strokeStyle = "#151515";
      ctx.lineWidth = 2;
      ctx.stroke();
    }
    if (won || active) {
      ctx.globalAlpha = won ? 0.46 : 0.28;
      ctx.strokeStyle = won ? "#fffdf7" : "#151515";
      ctx.lineWidth = won ? 3 : 2;
      ctx.beginPath();
      ctx.arc(point.x + 7, point.y + 3, (won ? 18 : 15) * mapPulse, 0, Math.PI * 2);
      ctx.stroke();
      ctx.globalAlpha = 1;
    }
    if (pending || (won && region.id === state.campaign.lastWonRegionId)) {
      drawMiniPerson(point.x - 18, point.y + 23, Math.max(12, state.mapRect.width * 0.032), {
        color: state.party.color,
        accent: "#fffdf7",
        flag: true,
        phase: point.x
      });
      drawMiniPerson(point.x + 22, point.y + 24, Math.max(10, state.mapRect.width * 0.028), {
        color: "#ffd166",
        accent: state.party.color,
        cheer: true,
        phase: point.y
      });
    }
    drawFlag(point.x, point.y, won ? "#d92d20" : pending ? "#ffd166" : "#151515", region.type === "UT" ? 0.5 : 0.66);
  }

  drawSelectedRegionCard(selectedRegion, width, height);
  drawNationalCompletion(width);
  drawMapLegend(width, height);

  ctx.fillStyle = "rgba(21, 21, 21, 0.88)";
  ctx.beginPath();
  ctx.roundRect(12, height - 66, Math.min(width - 24, 600), 48, 10);
  ctx.fill();
  ctx.fillStyle = "#fffdf7";
  ctx.font = `900 ${width < 430 ? 12 : 14}px ui-sans-serif`;
  ctx.textAlign = "left";
  const pendingRegion = REGIONS.find((region) => region.id === state.campaign.pendingRegionId);
  const guide = state.campaign.nationalWon
    ? "National mandate complete. World yatra coming soon."
    : pendingRegion
      ? `${pendingRegion.name} selected. OK dabao to arena khulega.`
      : "Black flag touch karo. Red flag already won hai.";
  ctx.fillText(guide, 26, height - 38, width - 52);
  ctx.restore();
}

function drawRegionArena() {
  ctx.save();
  ctx.translate(state.offsetX, state.offsetY);
  const s = state.cellSize;
  if (state.activePolygon.length > 2) {
    ctx.save();
    ctx.beginPath();
    state.activePolygon.forEach((point, i) => {
      const x = point.x * s;
      const y = point.y * s;
      if (i === 0) ctx.moveTo(x, y);
      else ctx.lineTo(x, y);
    });
    ctx.closePath();
    ctx.clip();
    const arenaGlow = ctx.createLinearGradient(0, 0, COLS * s, ROWS * s);
    arenaGlow.addColorStop(0, "rgba(255, 253, 247, 0.28)");
    arenaGlow.addColorStop(0.55, "rgba(255, 209, 102, 0.12)");
    arenaGlow.addColorStop(1, "rgba(14, 159, 138, 0.16)");
    ctx.fillStyle = arenaGlow;
    ctx.fillRect(0, 0, COLS * s, ROWS * s);
    ctx.strokeStyle = "rgba(255, 253, 247, 0.34)";
    ctx.lineWidth = Math.max(1, s * 0.12);
    for (let stripe = -ROWS; stripe < COLS; stripe += 6) {
      ctx.beginPath();
      ctx.moveTo(stripe * s, ROWS * s);
      ctx.lineTo((stripe + ROWS) * s, 0);
      ctx.stroke();
    }
    ctx.restore();
  }
  ctx.fillStyle = "rgba(21, 21, 21, 0.12)";
  for (let y = 0; y < ROWS; y += 1) {
    for (let x = 0; x < COLS; x += 1) {
      if (!state.regionMask[index(x, y)]) {
        ctx.fillRect(x * s, y * s, s + 0.5, s + 0.5);
      }
    }
  }
  ctx.lineJoin = "round";
  ctx.strokeStyle = "rgba(255, 253, 247, 0.82)";
  ctx.lineWidth = Math.max(5, s * 0.42);
  ctx.beginPath();
  state.activePolygon.forEach((point, i) => {
    const x = point.x * s;
    const y = point.y * s;
    if (i === 0) ctx.moveTo(x, y);
    else ctx.lineTo(x, y);
  });
  ctx.closePath();
  ctx.stroke();
  ctx.strokeStyle = "#151515";
  ctx.lineWidth = Math.max(3, s * 0.32);
  ctx.beginPath();
  state.activePolygon.forEach((point, i) => {
    const x = point.x * s;
    const y = point.y * s;
    if (i === 0) ctx.moveTo(x, y);
    else ctx.lineTo(x, y);
  });
  ctx.closePath();
  ctx.stroke();

  const activeRegion = getActiveRegion();
  if (activeRegion) {
    ctx.fillStyle = "rgba(21, 21, 21, 0.84)";
    ctx.font = `900 ${Math.max(12, s * 1.05)}px ui-sans-serif`;
    ctx.textAlign = "center";
    ctx.fillText(activeRegion.name, (COLS / 2) * s, 4.4 * s);
  }
  ctx.restore();
}

function drawNetaHud(width) {
  if (state.mode !== "playing") return;
  const score = mandateScore();
  const status = state.dangerCue ? "Danger" : state.roundStarted ? `Yatra ${score}` : "Ready";
  const label = `${status}  S ${state.neta.support}  F ${state.neta.funds}  P ${state.neta.power}  R ${state.neta.reputation}`;
  const boxW = Math.min(width - 20, 390);
  ctx.save();
  ctx.fillStyle = state.dangerCue ? "rgba(217, 45, 32, 0.9)" : state.roundStarted ? "rgba(21, 21, 21, 0.88)" : "rgba(14, 159, 138, 0.9)";
  ctx.strokeStyle = "rgba(255, 253, 247, 0.72)";
  ctx.lineWidth = 1;
  ctx.beginPath();
  ctx.roundRect(10, 10, boxW, 30, 8);
  ctx.fill();
  ctx.stroke();
  ctx.fillStyle = "#fffdf7";
  ctx.font = `900 ${width < 430 ? 11 : 12}px ui-sans-serif`;
  ctx.textAlign = "left";
  ctx.textBaseline = "middle";
  ctx.fillText(label, 22, 25, boxW - 24);
  ctx.restore();
}

function drawPauseOverlay(width, height) {
  if (state.mode !== "playing" || !state.paused) return;
  ctx.save();
  ctx.fillStyle = "rgba(21, 21, 21, 0.54)";
  ctx.fillRect(0, 0, width, height);
  const boxW = Math.min(width - 32, 300);
  const boxH = 106;
  const x = (width - boxW) / 2;
  const y = (height - boxH) / 2;
  ctx.fillStyle = "#fffdf7";
  ctx.strokeStyle = "#151515";
  ctx.lineWidth = 3;
  ctx.beginPath();
  ctx.roundRect(x, y, boxW, boxH, 10);
  ctx.fill();
  ctx.stroke();
  ctx.fillStyle = state.party.color;
  ctx.fillRect(x + 14, y + 14, 8, boxH - 28);
  ctx.fillStyle = "#151515";
  ctx.font = "900 24px ui-sans-serif";
  ctx.textAlign = "center";
  ctx.fillText("Campaign Paused", width / 2, y + 42);
  ctx.font = "800 13px ui-sans-serif";
  ctx.fillText("Tap Pause or press P to resume", width / 2, y + 70, boxW - 28);
  ctx.restore();
}

function drawReadyPulse() {
  if (state.mode !== "playing" || state.roundStarted || !state.player) return;
  const s = state.cellSize;
  const cx = state.offsetX + (state.player.x + 0.5) * s;
  const cy = state.offsetY + (state.player.y + 0.5) * s;
  const pulse = (Math.sin((state.lastTime || 0) * 0.006) + 1) / 2;
  const radius = s * (3.3 + pulse * 0.75);

  ctx.save();
  ctx.strokeStyle = `rgba(255, 209, 102, ${0.65 + pulse * 0.28})`;
  ctx.lineWidth = Math.max(3, s * 0.18);
  ctx.setLineDash([s * 0.8, s * 0.55]);
  ctx.beginPath();
  ctx.arc(cx, cy, radius, 0, Math.PI * 2);
  ctx.stroke();
  ctx.setLineDash([]);

  ctx.fillStyle = "rgba(21, 21, 21, 0.82)";
  const arrows = [
    [0, -1, 0],
    [1, 0, Math.PI / 2],
    [0, 1, Math.PI],
    [-1, 0, -Math.PI / 2]
  ];
  for (const [dx, dy, rotation] of arrows) {
    ctx.save();
    ctx.translate(cx + dx * radius * 0.92, cy + dy * radius * 0.92);
    ctx.rotate(rotation);
    ctx.beginPath();
    ctx.moveTo(0, -s * 0.48);
    ctx.lineTo(s * 0.38, s * 0.32);
    ctx.lineTo(-s * 0.38, s * 0.32);
    ctx.closePath();
    ctx.fill();
    ctx.restore();
  }
  ctx.restore();
}

function drawGridBackground(width, height) {
  const bg = ctx.createLinearGradient(0, 0, 0, height);
  bg.addColorStop(0, "#f7e8bd");
  bg.addColorStop(0.55, "#ebe0bf");
  bg.addColorStop(1, "#d8e8c4");
  ctx.fillStyle = bg;
  ctx.fillRect(0, 0, width, height);

  ctx.save();
  ctx.translate(state.offsetX, state.offsetY);
  const s = state.cellSize;
  ctx.fillStyle = "rgba(255, 253, 247, 0.3)";
  for (let y = 1; y < ROWS; y += 7) {
    ctx.fillRect(0, y * s, COLS * s, Math.max(1, s * 0.16));
  }
  ctx.fillStyle = "rgba(14, 159, 138, 0.12)";
  for (let i = 0; i < 90; i += 1) {
    const x = ((i * 17) % COLS + 0.3 + (i % 3) * 0.18) * s;
    const y = ((i * 29) % ROWS + 0.4) * s;
    if (!state.regionMask[index(clamp(Math.floor(x / s), 0, COLS - 1), clamp(Math.floor(y / s), 0, ROWS - 1))]) continue;
    ctx.beginPath();
    ctx.arc(x, y, Math.max(0.8, s * (0.045 + (i % 4) * 0.01)), 0, Math.PI * 2);
    ctx.fill();
  }
  ctx.strokeStyle = "rgba(21, 21, 21, 0.045)";
  ctx.lineWidth = 1;
  for (let x = 0; x <= COLS; x += 4) {
    ctx.beginPath();
    ctx.moveTo(x * s, 0);
    ctx.lineTo(x * s, ROWS * s);
    ctx.stroke();
  }
  for (let y = 0; y <= ROWS; y += 4) {
    ctx.beginPath();
    ctx.moveTo(0, y * s);
    ctx.lineTo(COLS * s, y * s);
    ctx.stroke();
  }
  ctx.restore();
}

function drawSymbol(symbol, cx, cy, size, color) {
  ctx.save();
  ctx.translate(cx, cy);
  ctx.strokeStyle = "#151515";
  ctx.fillStyle = color;
  ctx.lineWidth = Math.max(1.5, size * 0.12);
  if (symbol === "star") {
    ctx.beginPath();
    for (let i = 0; i < 10; i += 1) {
      const angle = -Math.PI / 2 + (i * Math.PI) / 5;
      const radius = i % 2 === 0 ? size * 0.48 : size * 0.2;
      const x = Math.cos(angle) * radius;
      const y = Math.sin(angle) * radius;
      if (i === 0) ctx.moveTo(x, y);
      else ctx.lineTo(x, y);
    }
    ctx.closePath();
    ctx.fill();
    ctx.stroke();
  } else if (symbol === "kite") {
    ctx.beginPath();
    ctx.moveTo(0, -size * 0.5);
    ctx.lineTo(size * 0.42, 0);
    ctx.lineTo(0, size * 0.52);
    ctx.lineTo(-size * 0.42, 0);
    ctx.closePath();
    ctx.fill();
    ctx.stroke();
  } else if (symbol === "mic") {
    ctx.fillRect(-size * 0.18, -size * 0.45, size * 0.36, size * 0.52);
    ctx.strokeRect(-size * 0.18, -size * 0.45, size * 0.36, size * 0.52);
    ctx.beginPath();
    ctx.moveTo(0, size * 0.08);
    ctx.lineTo(0, size * 0.46);
    ctx.moveTo(-size * 0.24, size * 0.46);
    ctx.lineTo(size * 0.24, size * 0.46);
    ctx.stroke();
  } else if (symbol === "cup") {
    ctx.fillRect(-size * 0.34, -size * 0.24, size * 0.56, size * 0.46);
    ctx.strokeRect(-size * 0.34, -size * 0.24, size * 0.56, size * 0.46);
    ctx.beginPath();
    ctx.arc(size * 0.22, 0, size * 0.2, -Math.PI / 2, Math.PI / 2);
    ctx.stroke();
  } else {
    ctx.beginPath();
    ctx.arc(0, 0, size * 0.44, 0, Math.PI * 2);
    ctx.fill();
    ctx.stroke();
    for (let i = 0; i < 8; i += 1) {
      const angle = (i * Math.PI) / 4;
      ctx.beginPath();
      ctx.moveTo(0, 0);
      ctx.lineTo(Math.cos(angle) * size * 0.44, Math.sin(angle) * size * 0.44);
      ctx.stroke();
    }
  }
  ctx.restore();
}

function drawTinyPoster(x, y, s, color, label = "NETA JI") {
  ctx.save();
  ctx.fillStyle = "#fffdf7";
  ctx.strokeStyle = "#151515";
  ctx.lineWidth = Math.max(1, s * 0.08);
  ctx.fillRect(x, y, s * 2.5, s * 1.32);
  ctx.strokeRect(x, y, s * 2.5, s * 1.32);
  ctx.fillStyle = color;
  ctx.fillRect(x + s * 0.12, y + s * 0.15, s * 0.44, s * 1.02);
  ctx.fillStyle = "#151515";
  ctx.font = `900 ${Math.max(7, s * 0.46)}px ui-sans-serif`;
  ctx.textAlign = "left";
  ctx.fillText(label, x + s * 0.68, y + s * 0.86);
  ctx.restore();
}

function drawTinyFlag(x, y, s, color) {
  ctx.save();
  ctx.strokeStyle = "#151515";
  ctx.lineWidth = Math.max(1, s * 0.1);
  ctx.beginPath();
  ctx.moveTo(x, y - s * 0.9);
  ctx.lineTo(x, y + s * 1.2);
  ctx.stroke();
  ctx.fillStyle = color;
  ctx.beginPath();
  ctx.moveTo(x, y - s * 0.9);
  ctx.lineTo(x + s * 1.18, y - s * 0.58);
  ctx.lineTo(x, y - s * 0.2);
  ctx.closePath();
  ctx.fill();
  ctx.stroke();
  ctx.restore();
}

function drawMiniPerson(cx, cy, size, options = {}) {
  const color = options.color || state.party.color;
  const accent = options.accent || "#fffdf7";
  const skin = options.skin || "#f0b37e";
  const hair = options.hair || "#151515";
  const trouser = options.trouser || shadeHex(color, -58);
  const phase = options.phase || 0;
  const bob = Math.sin((state.lastTime || 0) * 0.012 + phase) * size * 0.05;
  const walk = Math.sin((state.lastTime || 0) * 0.018 + phase);
  const bodyW = size * 0.54;
  const bodyH = size * 0.76;

  ctx.save();
  ctx.translate(cx, cy + bob);
  ctx.fillStyle = "rgba(21, 21, 21, 0.2)";
  ctx.beginPath();
  ctx.ellipse(0, size * 0.74, size * 0.5, size * 0.16, 0, 0, Math.PI * 2);
  ctx.fill();

  ctx.strokeStyle = "#151515";
  ctx.lineWidth = Math.max(1.2, size * 0.08);
  ctx.lineCap = "round";
  ctx.lineJoin = "round";

  ctx.beginPath();
  ctx.moveTo(-bodyW * 0.2, size * 0.42);
  ctx.lineTo(-bodyW * 0.36 + walk * size * 0.08, size * 0.82);
  ctx.moveTo(bodyW * 0.2, size * 0.42);
  ctx.lineTo(bodyW * 0.36 - walk * size * 0.08, size * 0.82);
  ctx.stroke();
  ctx.fillStyle = trouser;
  ctx.beginPath();
  ctx.ellipse(-bodyW * 0.38 + walk * size * 0.08, size * 0.86, size * 0.15, size * 0.055, 0, 0, Math.PI * 2);
  ctx.ellipse(bodyW * 0.38 - walk * size * 0.08, size * 0.86, size * 0.15, size * 0.055, 0, 0, Math.PI * 2);
  ctx.fill();

  ctx.fillStyle = color;
  ctx.beginPath();
  ctx.roundRect(-bodyW / 2, -size * 0.08, bodyW, bodyH, size * 0.12);
  ctx.fill();
  ctx.stroke();
  ctx.fillStyle = shadeHex(color, -24);
  ctx.fillRect(-bodyW / 2, size * 0.42, bodyW, size * 0.08);

  if (options.sash) {
    ctx.strokeStyle = accent;
    ctx.lineWidth = Math.max(1.2, size * 0.09);
    ctx.beginPath();
    ctx.moveTo(-bodyW * 0.42, -size * 0.02);
    ctx.lineTo(bodyW * 0.42, size * 0.58);
    ctx.stroke();
    ctx.strokeStyle = "#151515";
    ctx.lineWidth = Math.max(1.2, size * 0.08);
  }

  ctx.fillStyle = accent;
  ctx.beginPath();
  ctx.moveTo(-bodyW * 0.2, -size * 0.04);
  ctx.lineTo(bodyW * 0.2, -size * 0.04);
  ctx.lineTo(0, size * 0.34);
  ctx.closePath();
  ctx.fill();
  ctx.stroke();

  ctx.fillStyle = skin;
  if (options.foldedHands) {
    ctx.beginPath();
    ctx.moveTo(-bodyW * 0.48, size * 0.1);
    ctx.lineTo(-bodyW * 0.06, size * 0.35);
    ctx.moveTo(bodyW * 0.48, size * 0.1);
    ctx.lineTo(bodyW * 0.06, size * 0.35);
    ctx.stroke();
    ctx.beginPath();
    ctx.ellipse(-bodyW * 0.04, size * 0.36, size * 0.12, size * 0.18, -0.4, 0, Math.PI * 2);
    ctx.ellipse(bodyW * 0.04, size * 0.36, size * 0.12, size * 0.18, 0.4, 0, Math.PI * 2);
    ctx.fill();
    ctx.stroke();
  } else {
    const leftHandY = size * (options.cheer ? -0.18 : 0.36) + walk * size * 0.08;
    const rightHandY = size * (options.cheer ? -0.26 : 0.36) - walk * size * 0.08;
    ctx.beginPath();
    ctx.moveTo(-bodyW * 0.48, size * 0.02);
    ctx.lineTo(-bodyW * 0.76, leftHandY);
    ctx.moveTo(bodyW * 0.5, size * 0.04);
    ctx.lineTo(bodyW * 0.76, rightHandY);
    ctx.stroke();
    ctx.beginPath();
    ctx.arc(-bodyW * 0.76, leftHandY, size * 0.085, 0, Math.PI * 2);
    ctx.arc(bodyW * 0.76, rightHandY, size * 0.085, 0, Math.PI * 2);
    ctx.fill();
    ctx.stroke();
    if (options.mic) {
      ctx.strokeStyle = "#151515";
      ctx.fillStyle = "#151515";
      ctx.beginPath();
      ctx.moveTo(bodyW * 0.76, rightHandY);
      ctx.lineTo(bodyW * 1.04, rightHandY - size * 0.2);
      ctx.stroke();
      ctx.beginPath();
      ctx.arc(bodyW * 1.08, rightHandY - size * 0.23, size * 0.08, 0, Math.PI * 2);
      ctx.fill();
    }
  }

  if (options.poster) {
    drawTinyPoster(-size * 0.92, -size * 0.5, size * 0.34, color, options.poster === true ? "VOTE" : options.poster);
  }

  ctx.fillStyle = skin;
  ctx.beginPath();
  ctx.arc(0, -size * 0.42, size * 0.32, 0, Math.PI * 2);
  ctx.fill();
  ctx.stroke();
  ctx.beginPath();
  ctx.arc(-size * 0.32, -size * 0.43, size * 0.06, 0, Math.PI * 2);
  ctx.arc(size * 0.32, -size * 0.43, size * 0.06, 0, Math.PI * 2);
  ctx.fill();
  ctx.stroke();

  ctx.fillStyle = hair;
  ctx.beginPath();
  ctx.arc(0, -size * 0.52, size * 0.28, Math.PI, Math.PI * 2);
  ctx.fill();
  ctx.beginPath();
  ctx.moveTo(-size * 0.28, -size * 0.5);
  ctx.quadraticCurveTo(-size * 0.08, -size * 0.74, size * 0.22, -size * 0.53);
  ctx.stroke();

  if (options.cap) {
    ctx.fillStyle = accent;
    ctx.strokeStyle = "#151515";
    ctx.lineWidth = Math.max(1, size * 0.05);
    ctx.beginPath();
    ctx.arc(0, -size * 0.58, size * 0.26, Math.PI, Math.PI * 2);
    ctx.fill();
    ctx.stroke();
    ctx.beginPath();
    ctx.moveTo(size * 0.02, -size * 0.58);
    ctx.lineTo(size * 0.36, -size * 0.55);
    ctx.stroke();
  }

  ctx.fillStyle = "#151515";
  ctx.beginPath();
  ctx.arc(-size * 0.1, -size * 0.43, size * 0.035, 0, Math.PI * 2);
  ctx.arc(size * 0.1, -size * 0.43, size * 0.035, 0, Math.PI * 2);
  ctx.fill();
  ctx.strokeStyle = "#151515";
  ctx.lineWidth = Math.max(1, size * 0.035);
  ctx.beginPath();
  ctx.arc(0, -size * 0.33, size * 0.11, 0.15 * Math.PI, 0.85 * Math.PI);
  ctx.stroke();

  if (options.leaderMark) {
    ctx.fillStyle = accent;
    ctx.beginPath();
    ctx.roundRect(-size * 0.035, -size * 0.5, size * 0.07, size * 0.13, size * 0.03);
    ctx.fill();
  }

  if (options.symbol) {
    drawSymbol(options.symbol, 0, size * 0.2, size * 0.36, accent);
  }

  if (options.flag) {
    drawTinyFlag(size * 0.42, -size * 0.2, size * 0.32, color);
  }

  if (options.label) {
    ctx.fillStyle = "#151515";
    ctx.font = `900 ${Math.max(7, size * 0.34)}px ui-sans-serif`;
    ctx.textAlign = "center";
    ctx.fillText(options.label, 0, size * 1.12);
  }
  ctx.restore();
}

function drawTerritory() {
  ctx.save();
  ctx.translate(state.offsetX, state.offsetY);
  const s = state.cellSize;
  for (let y = 0; y < ROWS; y += 1) {
    for (let x = 0; x < COLS; x += 1) {
      const idx = index(x, y);
      if (!state.regionMask[idx]) continue;
      const ownerId = state.owner[idx];
      if (!ownerId) continue;
      const baseColor = ownerColor(ownerId);
      ctx.fillStyle = baseColor;
      ctx.globalAlpha = ownerId === 1 ? 0.82 : 0.62;
      ctx.fillRect(x * s, y * s, s + 0.5, s + 0.5);
      ctx.globalAlpha = ownerId === 1 ? 0.16 : 0.1;
      ctx.fillStyle = ownerId === 1 ? "#fffdf7" : shadeHex(baseColor, 34);
      ctx.fillRect(x * s + s * 0.62, y * s + s * 0.1, Math.max(1, s * 0.11), s * 0.8);
      if (ownerId === 1 && (x + y) % 13 === 0) {
        ctx.globalAlpha = 0.18;
        ctx.fillStyle = "#151515";
        ctx.beginPath();
        ctx.arc(x * s + s * 0.32, y * s + s * 0.38, Math.max(1, s * 0.08), 0, Math.PI * 2);
        ctx.fill();
      }

      if (ownerId === 1 && x % 8 === 2 && y % 6 === 2) {
        ctx.globalAlpha = 1;
        drawTinyPoster(x * s + s * 0.1, y * s + s * 0.18, s, state.party.color);
      }

      if (ownerId === 1 && x % 11 === 6 && y % 7 === 4) {
        ctx.globalAlpha = 1;
        drawTinyFlag(x * s + s * 0.4, y * s + s * 0.9, s * 0.8, state.party.color);
      }
    }
  }
  ctx.globalAlpha = 1;
  ctx.restore();
}

function drawTrails() {
  ctx.save();
  ctx.translate(state.offsetX, state.offsetY);
  const s = state.cellSize;
  for (let y = 0; y < ROWS; y += 1) {
    for (let x = 0; x < COLS; x += 1) {
      const ownerId = state.trail[index(x, y)];
      if (!ownerId) continue;
      const baseColor = ownerColor(ownerId);
      const isPlayerTrail = ownerId === 1;
      ctx.fillStyle = isPlayerTrail ? shadeHex(baseColor, -55) : baseColor;
      ctx.globalAlpha = isPlayerTrail ? 1 : 0.8;
      ctx.shadowColor = isPlayerTrail ? "rgba(21, 21, 21, 0.24)" : "transparent";
      ctx.shadowBlur = isPlayerTrail ? Math.max(2, s * 0.18) : 0;
      ctx.beginPath();
      ctx.roundRect(x * s + s * 0.07, y * s + s * 0.07, s * 0.86, s * 0.86, Math.max(2, s * 0.18));
      ctx.fill();
      ctx.shadowBlur = 0;
      if (isPlayerTrail) {
        ctx.strokeStyle = "rgba(21, 21, 21, 0.72)";
        ctx.lineWidth = Math.max(1, s * 0.12);
        ctx.stroke();
        ctx.strokeStyle = "rgba(255, 253, 247, 0.42)";
        ctx.lineWidth = Math.max(1, s * 0.06);
        ctx.beginPath();
        ctx.moveTo(x * s + s * 0.24, y * s + s * 0.28);
        ctx.lineTo(x * s + s * 0.74, y * s + s * 0.72);
        ctx.stroke();
      }
    }
  }
  ctx.restore();
  ctx.globalAlpha = 1;
}

function drawTrailPaths() {
  const agents = [state.player, ...state.opponents].filter(Boolean);
  ctx.save();
  ctx.translate(state.offsetX, state.offsetY);
  const s = state.cellSize;
  ctx.lineCap = "round";
  ctx.lineJoin = "round";
  for (const agent of agents) {
    if (!agent.trailPoints || agent.trailPoints.length < 2) continue;
    const isPlayerTrail = agent.ownerId === 1;
    const color = isPlayerTrail ? shadeHex(ownerColor(agent.ownerId), -62) : ownerColor(agent.ownerId);
    ctx.globalAlpha = isPlayerTrail ? 0.96 : 0.72;
    ctx.strokeStyle = color;
    ctx.lineWidth = isPlayerTrail ? s * 0.86 : s * 0.64;
    ctx.shadowColor = isPlayerTrail ? "rgba(21, 21, 21, 0.22)" : "transparent";
    ctx.shadowBlur = isPlayerTrail ? Math.max(2, s * 0.22) : 0;
    ctx.beginPath();
    agent.trailPoints.forEach((point, i) => {
      const x = (point.x + 0.5) * s;
      const y = (point.y + 0.5) * s;
      if (i === 0) ctx.moveTo(x, y);
      else ctx.lineTo(x, y);
    });
    ctx.stroke();
    ctx.shadowBlur = 0;
    if (isPlayerTrail) {
      ctx.globalAlpha = 0.72;
      ctx.strokeStyle = "rgba(21, 21, 21, 0.62)";
      ctx.lineWidth = Math.max(1, s * 0.12);
      ctx.stroke();
      ctx.globalAlpha = 0.52;
      ctx.strokeStyle = "rgba(255, 253, 247, 0.72)";
      ctx.lineWidth = Math.max(1, s * 0.08);
      ctx.setLineDash([s * 0.42, s * 0.56]);
      ctx.stroke();
      ctx.setLineDash([]);
    }
  }
  ctx.restore();
  ctx.globalAlpha = 1;
}

function drawCampaignRoads() {
  if (state.mode !== "playing") return;
  ctx.save();
  ctx.translate(state.offsetX, state.offsetY);
  const s = state.cellSize;
  const route = [
    findMaskedSpawn(10, 11),
    findMaskedSpawn(22, 16),
    findMaskedSpawn(35, 12),
    findMaskedSpawn(49, 21),
    findMaskedSpawn(41, 31),
    findMaskedSpawn(20, 29),
    findMaskedSpawn(10, 20)
  ];

  ctx.lineCap = "round";
  ctx.lineJoin = "round";
  ctx.strokeStyle = "rgba(117, 103, 82, 0.36)";
  ctx.lineWidth = Math.max(5, s * 1.05);
  ctx.beginPath();
  route.forEach((point, i) => {
    const x = (point.x + 0.5) * s;
    const y = (point.y + 0.5) * s;
    if (i === 0) ctx.moveTo(x, y);
    else ctx.lineTo(x, y);
  });
  ctx.stroke();

  ctx.setLineDash([s * 0.85, s * 0.75]);
  ctx.strokeStyle = "rgba(255, 253, 247, 0.7)";
  ctx.lineWidth = Math.max(1.2, s * 0.12);
  ctx.stroke();
  ctx.setLineDash([]);

  for (const checkpoint of [route[1], route[3], route[5]]) {
    drawTinyFlag((checkpoint.x + 0.5) * s, (checkpoint.y + 0.45) * s, s * 0.55, state.party.color);
  }

  if (state.roundStarted) {
    const stepCount = 9;
    for (let i = 0; i < stepCount; i += 1) {
      const routeIndex = (i + state.roundElapsed * 1.8) % (route.length - 1);
      const segment = Math.floor(routeIndex);
      const t = routeIndex - segment;
      const from = route[segment];
      const to = route[segment + 1];
      const x = (from.x + (to.x - from.x) * t + 0.5) * s;
      const y = (from.y + (to.y - from.y) * t + 0.5) * s;
      const sideX = Math.sign(to.y - from.y) * s * 0.12;
      const sideY = -Math.sign(to.x - from.x) * s * 0.12;
      ctx.globalAlpha = 0.84;
      drawMiniPerson(x - sideX, y - sideY, s * 0.58, {
        color: i % 2 ? state.party.color : "#fffdf7",
        accent: i % 2 ? "#fffdf7" : state.party.color,
        phase: i * 0.7 + state.roundElapsed,
        cheer: i % 3 === 0
      });
      drawMiniPerson(x + sideX, y + sideY, s * 0.48, {
        color: i % 2 ? "#ffd166" : state.party.color,
        accent: "#fffdf7",
        phase: i * 0.9 + state.roundElapsed,
        flag: i % 4 === 0
      });
    }
    ctx.globalAlpha = 1;
  }

  ctx.restore();
}

function drawSupporters() {
  if (state.supporters.length === 0) return;
  ctx.save();
  ctx.translate(state.offsetX, state.offsetY);
  const s = state.cellSize;
  state.supporters.forEach((supporter, i) => {
    const cx = (supporter.x + 0.5) * s;
    const cy = (supporter.y + 0.5) * s;
    drawMiniPerson(cx, cy, s * (1.45 + supporter.size), {
      color: supporter.color,
      accent: "#fffdf7",
      phase: supporter.phase + i * 0.8,
      flag: i % 4 === 0,
      poster: i % 7 === 3,
      cheer: i % 5 === 1,
      cap: i % 3 === 0,
      sash: i % 6 === 2
    });
  });
  ctx.restore();
  ctx.globalAlpha = 1;
}

function drawConversionBursts() {
  if (state.conversionBursts.length === 0) return;
  ctx.save();
  ctx.translate(state.offsetX, state.offsetY);
  const s = state.cellSize;
  for (const particle of state.conversionBursts) {
    const alpha = clamp(particle.life / particle.maxLife, 0, 1);
    ctx.globalAlpha = alpha;
    ctx.fillStyle = particle.color;
    ctx.strokeStyle = "rgba(21, 21, 21, 0.72)";
    ctx.lineWidth = Math.max(1, s * 0.08);
    ctx.beginPath();
    ctx.arc((particle.x + 0.5) * s, (particle.y + 0.5) * s, s * particle.size, 0, Math.PI * 2);
    ctx.fill();
    ctx.stroke();
  }
  ctx.restore();
  ctx.globalAlpha = 1;
}

function drawClaimBursts() {
  if (state.claimBursts.length === 0) return;
  ctx.save();
  ctx.translate(state.offsetX, state.offsetY);
  const s = state.cellSize;
  for (const burst of state.claimBursts) {
    if (burst.delay > 0) continue;
    const progress = 1 - burst.life / burst.maxLife;
    const alpha = clamp(burst.life / burst.maxLife, 0, 1);
    ctx.globalAlpha = alpha * 0.75;
    ctx.fillStyle = burst.color;
    ctx.strokeStyle = "rgba(21, 21, 21, 0.38)";
    ctx.lineWidth = Math.max(1, s * 0.08);
    ctx.beginPath();
    ctx.arc(burst.x * s, burst.y * s, s * (burst.size + progress * 0.9), 0, Math.PI * 2);
    ctx.fill();
    ctx.stroke();
  }
  ctx.restore();
  ctx.globalAlpha = 1;
}

function drawRaidScene(scene, s) {
  const x = (scene.x + 0.5) * s;
  const y = (scene.y + 0.5) * s;
  const pulse = Math.sin(scene.phase) * s * 0.18;

  ctx.save();
  ctx.globalAlpha = clamp(scene.life / scene.maxLife, 0, 1);
  ctx.fillStyle = "rgba(21, 21, 21, 0.18)";
  ctx.beginPath();
  ctx.ellipse(x, y + s * 1.15, s * 3.1, s * 1.1, 0, 0, Math.PI * 2);
  ctx.fill();

  ctx.fillStyle = "#fffdf7";
  ctx.strokeStyle = "#151515";
  ctx.lineWidth = Math.max(1, s * 0.09);
  ctx.beginPath();
  ctx.roundRect(x - s * 1.2, y - s * 1.05, s * 2.4, s * 1.55, s * 0.14);
  ctx.fill();
  ctx.stroke();
  ctx.fillStyle = "#ffcfbf";
  ctx.fillRect(x - s * 1.05, y - s * 0.88, s * 0.48, s * 1.2);
  ctx.strokeRect(x - s * 1.05, y - s * 0.88, s * 0.48, s * 1.2);
  ctx.fillStyle = "#151515";
  ctx.font = `900 ${Math.max(7, s * 0.52)}px ui-sans-serif`;
  ctx.textAlign = "center";
  ctx.fillText("CHECK", x + s * 0.35, y - s * 0.22, s * 1.45);

  for (let i = 0; i < 4; i += 1) {
    const px = x - s * 1.05 + i * s * 0.62;
    const py = y + s * 0.78 + Math.sin(scene.phase + i) * s * 0.08;
    ctx.fillStyle = "#fffdf7";
    ctx.strokeStyle = "#151515";
    ctx.fillRect(px, py, s * 0.42, s * 0.54);
    ctx.strokeRect(px, py, s * 0.42, s * 0.54);
    ctx.beginPath();
    ctx.moveTo(px + s * 0.08, py + s * 0.18);
    ctx.lineTo(px + s * 0.34, py + s * 0.18);
    ctx.moveTo(px + s * 0.08, py + s * 0.34);
    ctx.lineTo(px + s * 0.3, py + s * 0.34);
    ctx.stroke();
  }

  drawMiniPerson(x - s * 2.05, y + s * 0.7 + pulse, s * 1.05, {
    color: "#2b2823",
    accent: "#ffd166",
    phase: scene.phase,
    foldedHands: false
  });
  drawMiniPerson(x + s * 2.05, y + s * 0.7 - pulse, s * 1.05, {
    color: "#625a4e",
    accent: "#fffdf7",
    phase: scene.phase + 1.2
  });
  ctx.restore();
  ctx.globalAlpha = 1;
}

function drawTeaBreakScene(scene, s) {
  const x = (scene.x + 0.5) * s;
  const y = (scene.y + 0.5) * s;
  const steam = Math.sin(scene.phase) * s * 0.12;

  ctx.save();
  ctx.globalAlpha = clamp(scene.life / scene.maxLife, 0, 1);
  ctx.fillStyle = "rgba(21, 21, 21, 0.16)";
  ctx.beginPath();
  ctx.ellipse(x, y + s * 1.25, s * 3.4, s * 1.08, 0, 0, Math.PI * 2);
  ctx.fill();

  ctx.fillStyle = "#fffdf7";
  ctx.strokeStyle = "#151515";
  ctx.lineWidth = Math.max(1, s * 0.09);
  ctx.fillRect(x - s * 1.25, y - s * 0.22, s * 2.5, s * 1.2);
  ctx.strokeRect(x - s * 1.25, y - s * 0.22, s * 2.5, s * 1.2);
  ctx.fillStyle = scene.color;
  ctx.beginPath();
  ctx.moveTo(x - s * 1.55, y - s * 0.28);
  ctx.lineTo(x, y - s * 1.25);
  ctx.lineTo(x + s * 1.55, y - s * 0.28);
  ctx.closePath();
  ctx.fill();
  ctx.stroke();
  ctx.fillStyle = "#151515";
  ctx.font = `900 ${Math.max(7, s * 0.5)}px ui-sans-serif`;
  ctx.textAlign = "center";
  ctx.fillText("CHAI", x, y + s * 0.5, s * 1.7);

  for (let i = 0; i < 3; i += 1) {
    const cupX = x - s * 0.62 + i * s * 0.62;
    ctx.fillStyle = "#f7c68f";
    ctx.fillRect(cupX, y + s * 0.88, s * 0.28, s * 0.32);
    ctx.strokeRect(cupX, y + s * 0.88, s * 0.28, s * 0.32);
    ctx.strokeStyle = "rgba(21, 21, 21, 0.42)";
    ctx.beginPath();
    ctx.moveTo(cupX + s * 0.08, y + s * 0.78 + steam);
    ctx.quadraticCurveTo(cupX + s * 0.22, y + s * 0.6, cupX + s * 0.12, y + s * 0.44 + steam);
    ctx.stroke();
  }

  drawMiniPerson(x - s * 2.1, y + s * 0.8, s * 0.92, {
    color: state.party.color,
    accent: "#fffdf7",
    phase: scene.phase,
    flag: true
  });
  drawMiniPerson(x + s * 2.1, y + s * 0.85, s * 0.92, {
    color: "#fffdf7",
    accent: state.party.color,
    phase: scene.phase + 1
  });
  ctx.restore();
  ctx.globalAlpha = 1;
}

function drawPosterRainScene(scene, s) {
  const x = (scene.x + 0.5) * s;
  const y = (scene.y + 0.5) * s;
  const alpha = clamp(scene.life / scene.maxLife, 0, 1);

  ctx.save();
  ctx.globalAlpha = alpha;
  ctx.fillStyle = "rgba(21, 21, 21, 0.14)";
  ctx.beginPath();
  ctx.ellipse(x, y + s * 1.1, s * 3.6, s * 1.2, 0, 0, Math.PI * 2);
  ctx.fill();

  for (let i = 0; i < 9; i += 1) {
    const fall = ((scene.maxLife - scene.life) * (1.8 + i * 0.12) + i * 0.17) % 1;
    const px = x - s * 2.7 + i * s * 0.68 + Math.sin(scene.phase + i) * s * 0.16;
    const py = y - s * 1.8 + fall * s * 3.1;
    ctx.save();
    ctx.translate(px, py);
    ctx.rotate(Math.sin(scene.phase + i) * 0.28);
    drawTinyPoster(-s * 0.32, -s * 0.18, s * 0.34, scene.color, i % 2 ? "JI" : "VOTE");
    ctx.restore();
  }

  drawMiniPerson(x - s * 1.6, y + s * 0.92, s * 0.96, {
    color: state.party.color,
    accent: "#fffdf7",
    cheer: true,
    phase: scene.phase
  });
  drawMiniPerson(x + s * 1.7, y + s * 0.92, s * 0.96, {
    color: "#ffd166",
    accent: state.party.color,
    poster: true,
    phase: scene.phase + 1.3
  });
  ctx.restore();
  ctx.globalAlpha = 1;
}

function drawMemeWaveScene(scene, s) {
  const x = (scene.x + 0.5) * s;
  const y = (scene.y + 0.5) * s;
  const alpha = clamp(scene.life / scene.maxLife, 0, 1);
  const wave = (scene.maxLife - scene.life) * 1.2;
  const captions = ["LOL", "TREND", "SHARE"];

  ctx.save();
  ctx.globalAlpha = alpha;
  for (let i = 0; i < 3; i += 1) {
    ctx.strokeStyle = `rgba(255, 209, 102, ${0.34 - i * 0.07})`;
    ctx.lineWidth = Math.max(2, s * 0.16);
    ctx.beginPath();
    ctx.arc(x, y, s * (1.2 + i * 0.9 + wave), 0, Math.PI * 2);
    ctx.stroke();
  }

  captions.forEach((caption, i) => {
    const angle = scene.phase + i * 2.05 + wave * 0.8;
    const px = x + Math.cos(angle) * s * (2.1 + i * 0.28);
    const py = y + Math.sin(angle) * s * (1.3 + i * 0.18);
    ctx.fillStyle = "#fffdf7";
    ctx.strokeStyle = "#151515";
    ctx.lineWidth = Math.max(1, s * 0.08);
    ctx.beginPath();
    ctx.roundRect(px - s * 0.85, py - s * 0.38, s * 1.7, s * 0.76, s * 0.2);
    ctx.fill();
    ctx.stroke();
    ctx.fillStyle = i === 1 ? state.party.color : "#151515";
    ctx.font = `900 ${Math.max(7, s * 0.42)}px ui-sans-serif`;
    ctx.textAlign = "center";
    ctx.textBaseline = "middle";
    ctx.fillText(caption, px, py + s * 0.02, s * 1.35);
  });

  drawMiniPerson(x - s * 1.1, y + s * 0.82, s * 0.96, {
    color: state.party.color,
    accent: "#fffdf7",
    cheer: true,
    mic: true,
    cap: true,
    phase: scene.phase
  });
  drawMiniPerson(x + s * 1.2, y + s * 0.84, s * 0.9, {
    color: "#fffdf7",
    accent: state.party.color,
    poster: "MEME",
    cheer: true,
    phase: scene.phase + 1.4
  });
  ctx.restore();
  ctx.globalAlpha = 1;
}

function drawDholBoostScene(scene, s) {
  const x = (scene.x + 0.5) * s;
  const y = (scene.y + 0.5) * s;
  const alpha = clamp(scene.life / scene.maxLife, 0, 1);
  const beat = Math.sin((state.lastTime || 0) * 0.026 + scene.phase);

  ctx.save();
  ctx.globalAlpha = alpha;
  ctx.strokeStyle = "rgba(21, 21, 21, 0.28)";
  ctx.lineWidth = Math.max(1.2, s * 0.09);
  for (let i = 0; i < 4; i += 1) {
    ctx.beginPath();
    ctx.arc(x, y, s * (0.75 + i * 0.5 + Math.abs(beat) * 0.2), 0.15 * Math.PI, 0.85 * Math.PI);
    ctx.stroke();
  }

  ctx.fillStyle = "#ffd166";
  ctx.strokeStyle = "#151515";
  ctx.lineWidth = Math.max(1, s * 0.12);
  ctx.beginPath();
  ctx.ellipse(x, y + s * 0.2, s * (0.78 + Math.abs(beat) * 0.08), s * 0.52, 0, 0, Math.PI * 2);
  ctx.fill();
  ctx.stroke();
  ctx.fillStyle = state.party.color;
  ctx.fillRect(x - s * 0.18, y - s * 0.28, s * 0.36, s * 0.96);
  ctx.strokeRect(x - s * 0.18, y - s * 0.28, s * 0.36, s * 0.96);

  drawMiniPerson(x - s * 1.85, y + s * 0.84, s * 0.96, {
    color: state.party.color,
    accent: "#fffdf7",
    cheer: true,
    cap: true,
    phase: scene.phase
  });
  drawMiniPerson(x + s * 1.85, y + s * 0.84, s * 0.96, {
    color: "#ffd166",
    accent: state.party.color,
    cheer: true,
    sash: true,
    phase: scene.phase + 1.1
  });
  ctx.restore();
  ctx.globalAlpha = 1;
}

function drawEventScenes() {
  if (state.eventScenes.length === 0) return;
  ctx.save();
  ctx.translate(state.offsetX, state.offsetY);
  const s = state.cellSize;
  for (const scene of state.eventScenes) {
    if (scene.type === "raid") drawRaidScene(scene, s);
    if (scene.type === "teaBreak") drawTeaBreakScene(scene, s);
    if (scene.type === "posterRain") drawPosterRainScene(scene, s);
    if (scene.type === "memeWave") drawMemeWaveScene(scene, s);
    if (scene.type === "dholBoost") drawDholBoostScene(scene, s);
  }
  ctx.restore();
  ctx.globalAlpha = 1;
}

function drawCampaignProps() {
  if (state.mode !== "playing") return;
  ctx.save();
  ctx.translate(state.offsetX, state.offsetY);
  const s = state.cellSize;
  const props = [
    { x: 18, y: 14, type: "booth" },
    { x: 45, y: 13, type: "stage" },
    { x: 20, y: 30, type: "poster" },
    { x: 47, y: 30, type: "booth" }
  ];
  for (const prop of props) {
    const spot = findMaskedSpawn(prop.x, prop.y);
    const px = spot.x * s;
    const py = spot.y * s;
    if (prop.type === "stage") {
      ctx.fillStyle = "rgba(21, 21, 21, 0.18)";
      ctx.fillRect(px - s * 2, py + s * 1.6, s * 4.4, s * 0.48);
      ctx.fillStyle = "#fffdf7";
      ctx.strokeStyle = "#151515";
      ctx.lineWidth = Math.max(1, s * 0.1);
      ctx.fillRect(px - s * 2, py - s * 1.1, s * 4.2, s * 2.4);
      ctx.strokeRect(px - s * 2, py - s * 1.1, s * 4.2, s * 2.4);
      drawTinyFlag(px - s * 1.5, py - s * 0.1, s * 0.85, state.party.color);
      drawTinyFlag(px + s * 1.5, py - s * 0.1, s * 0.85, state.party.color);
      ctx.fillStyle = "#151515";
      ctx.font = `900 ${Math.max(7, s * 0.52)}px ui-sans-serif`;
      ctx.textAlign = "center";
      ctx.fillText("RALLY", px + s * 0.1, py + s * 0.25);
      const crowdCount = state.neta.support > 70 ? 7 : state.neta.support > 52 ? 5 : 3;
      for (let i = 0; i < crowdCount; i += 1) {
        const row = i % 2;
        const crowdX = px - s * 2.4 + i * s * 0.82;
        const crowdY = py + s * (2.55 + row * 0.55);
        drawMiniPerson(crowdX, crowdY, s * 1.06, {
          color: i % 3 === 0 ? state.party.color : "#fffdf7",
          accent: i % 3 === 0 ? "#fffdf7" : state.party.color,
          phase: i * 0.7,
          flag: i % 4 === 1,
          cap: i % 2 === 0,
          sash: i % 3 === 0
        });
      }
    } else if (prop.type === "booth") {
      ctx.fillStyle = "#fffdf7";
      ctx.strokeStyle = "#151515";
      ctx.lineWidth = Math.max(1, s * 0.1);
      ctx.fillRect(px - s * 1.1, py - s * 0.9, s * 2.2, s * 1.8);
      ctx.strokeRect(px - s * 1.1, py - s * 0.9, s * 2.2, s * 1.8);
      ctx.fillStyle = state.party.color;
      ctx.fillRect(px - s * 0.95, py - s * 0.75, s * 0.42, s * 1.5);
      ctx.fillStyle = "#151515";
      ctx.font = `900 ${Math.max(7, s * 0.45)}px ui-sans-serif`;
      ctx.textAlign = "left";
      ctx.fillText("BOOTH", px - s * 0.42, py + s * 0.2);
      if (state.neta.power > 24) {
        drawMiniPerson(px + s * 1.65, py + s * 0.55, s * 0.95, {
          color: state.party.color,
          accent: "#fffdf7",
          phase: prop.x,
          flag: true,
          cap: true
        });
      }
      const queueCount = state.neta.support > 64 ? 5 : state.neta.support > 48 ? 4 : 3;
      for (let i = 0; i < queueCount; i += 1) {
        drawMiniPerson(px - s * (2.15 + i * 0.68), py + s * (0.58 + (i % 2) * 0.12), s * 0.72, {
          color: i % 2 ? "#fffdf7" : state.party.color,
          accent: i % 2 ? state.party.color : "#fffdf7",
          phase: i * 0.9 + prop.y,
          cap: i % 3 === 0
        });
      }
    } else {
      drawTinyPoster(px - s * 1.2, py - s * 0.6, s, state.party.color, "VOTE");
      drawMiniPerson(px + s * 1.85, py + s * 0.85, s * 0.82, {
        color: state.party.color,
        accent: "#fffdf7",
        phase: prop.x + prop.y,
        flag: true,
        cap: true,
        sash: true
      });
    }
  }
  ctx.restore();
}

function drawAmbientPeople() {
  if (state.ambientPeople.length === 0) return;
  ctx.save();
  ctx.translate(state.offsetX, state.offsetY);
  const s = state.cellSize;
  const visibleCount = clamp(Math.round(12 + state.neta.support * 0.16 + state.influence * 0.1), 12, state.ambientPeople.length);

  for (let i = 0; i < visibleCount; i += 1) {
    const person = state.ambientPeople[i];
    const cellX = clamp(Math.floor(person.x), 0, COLS - 1);
    const cellY = clamp(Math.floor(person.y), 0, ROWS - 1);
    const ownerId = state.owner[index(cellX, cellY)];
    const isPartyArea = ownerId === 1;
    const isOpponentArea = ownerId > 1;
    if (isOpponentArea && person.role !== "voter" && i % 2 === 0) continue;

    const cx = (person.x + 0.5) * s;
    const cy = (person.y + 0.5) * s;
    const clothing = isPartyArea || person.role === "volunteer" || person.role === "flag" ? state.party.color : person.color;
    const accent = clothing === state.party.color ? "#fffdf7" : state.party.color;

    if (person.role === "poster" && isPartyArea) {
      drawTinyPoster(cx - s * 1.2, cy - s * 1.4, s * 0.62, state.party.color, "VOTE");
    }
    if (person.role === "camera") {
      ctx.fillStyle = "#151515";
      ctx.fillRect(cx + s * 0.34, cy - s * 0.92, s * 0.42, s * 0.3);
      ctx.fillStyle = "#bfe7ff";
      ctx.fillRect(cx + s * 0.42, cy - s * 0.85, s * 0.18, s * 0.13);
    }
    if (person.role === "dhol") {
      ctx.fillStyle = "#ffd166";
      ctx.strokeStyle = "#151515";
      ctx.lineWidth = Math.max(1, s * 0.08);
      ctx.beginPath();
      ctx.ellipse(cx + s * 0.42, cy + s * 0.12, s * 0.28, s * 0.2, 0, 0, Math.PI * 2);
      ctx.fill();
      ctx.stroke();
    }

    drawMiniPerson(cx, cy, s * person.size, {
      color: clothing,
      accent,
      phase: person.phase + i * 0.23,
      flag: person.role === "flag" || (isPartyArea && i % 9 === 0),
      poster: person.role === "poster" && !isPartyArea,
      cheer: person.role === "dhol" || (isPartyArea && i % 7 === 0),
      foldedHands: person.role === "voter" && state.influence > 42 && i % 10 === 0,
      cap: person.role === "volunteer" || person.role === "flag" || person.role === "dhol",
      sash: isPartyArea && i % 5 === 0,
      mic: person.role === "camera"
    });
  }

  ctx.restore();
  ctx.globalAlpha = 1;
}

function drawOpponentSquad(agent, cx, cy, s) {
  if (!agent.squad || agent.squad.length === 0) return;
  const dirX = agent.dirX || (agent.ownerId % 2 === 0 ? 1 : -1);
  const dirY = agent.dirY || 0;
  const sideX = -dirY || 0;
  const sideY = dirX || 1;
  const backX = -dirX;
  const backY = -dirY;

  agent.squad.forEach((member, i) => {
    const wobble = Math.sin((state.lastTime || 0) * 0.004 + member.phase) * s * 0.12;
    const mx = cx + backX * member.back * s + sideX * member.side * s + wobble;
    const my = cy + backY * member.back * s + sideY * member.side * s - wobble * 0.35;
    drawMiniPerson(mx, my, s * member.size, {
      color: i % 2 === 0 ? agent.color : "#fffdf7",
      accent: i % 2 === 0 ? "#fffdf7" : agent.color,
      phase: member.phase,
      flag: member.flag
    });
  });
}

function drawDangerCue() {
  if (state.mode !== "playing" || !state.dangerCue) return;
  const cue = state.dangerCue;
  const alpha = clamp(cue.life / cue.maxLife, 0, 1);
  const s = state.cellSize;
  const x = state.offsetX + (cue.x + 0.5) * s;
  const y = state.offsetY + (cue.y + 0.5) * s;
  const pulse = 1 + Math.sin((state.lastTime || 0) * 0.018) * 0.12;

  ctx.save();
  ctx.globalAlpha = alpha;
  ctx.strokeStyle = "#d92d20";
  ctx.fillStyle = "rgba(217, 45, 32, 0.18)";
  ctx.lineWidth = Math.max(2, s * 0.16);
  ctx.beginPath();
  ctx.arc(x, y, s * (1.25 + cue.level * 1.3) * pulse, 0, Math.PI * 2);
  ctx.fill();
  ctx.stroke();

  ctx.fillStyle = "#ffd166";
  ctx.strokeStyle = "#151515";
  ctx.lineWidth = Math.max(1.2, s * 0.08);
  ctx.beginPath();
  ctx.moveTo(x, y - s * 1.42);
  ctx.lineTo(x + s * 1.18, y + s * 0.72);
  ctx.lineTo(x - s * 1.18, y + s * 0.72);
  ctx.closePath();
  ctx.fill();
  ctx.stroke();
  ctx.fillStyle = "#151515";
  ctx.font = `900 ${Math.max(12, s * 1.1)}px ui-sans-serif`;
  ctx.textAlign = "center";
  ctx.textBaseline = "middle";
  ctx.fillText("!", x, y + s * 0.02);
  ctx.restore();
  ctx.globalAlpha = 1;
}

function drawAgent(agent, isPlayer) {
  const s = state.cellSize;
  const cx = state.offsetX + (agent.x + 0.5) * s;
  const cy = state.offsetY + (agent.y + 0.5) * s;
  ctx.save();
  if (!isPlayer) drawOpponentSquad(agent, cx, cy, s);
  if (isPlayer && state.roundStarted) {
    const speedPulse = state.speedMul > 1 ? 1.35 : 1;
    ctx.globalAlpha = 0.22;
    ctx.fillStyle = "#151515";
    for (let i = 0; i < 4; i += 1) {
      const back = (i + 1) * s * 0.42 * speedPulse;
      const side = (i % 2 ? -1 : 1) * s * 0.18;
      const dustX = cx - agent.dirX * back - agent.dirY * side;
      const dustY = cy - agent.dirY * back + agent.dirX * side;
      ctx.beginPath();
      ctx.ellipse(dustX, dustY + s * 1.12, Math.max(1.5, s * (0.16 - i * 0.018)), Math.max(1, s * 0.07), 0, 0, Math.PI * 2);
      ctx.fill();
    }
    ctx.globalAlpha = 1;
  }
  drawMiniPerson(cx, cy, s * (isPlayer ? 2.55 : 2.12), {
    color: agent.color,
    accent: "#fffdf7",
    symbol: agent.symbol,
    phase: agent.ownerId * 1.7,
    foldedHands: isPlayer,
    flag: !isPlayer,
    leaderMark: isPlayer
  });
  if (!isPlayer) {
    ctx.fillStyle = "rgba(21, 21, 21, 0.84)";
    ctx.strokeStyle = "#fffdf7";
    ctx.lineWidth = Math.max(1, s * 0.08);
    const tagW = Math.min(s * 7.2, Math.max(s * 4.4, agent.name.length * s * 0.34));
    const tagX = cx - tagW / 2;
    const tagY = cy - s * 3.25;
    ctx.beginPath();
    ctx.roundRect(tagX, tagY, tagW, s * 1.15, s * 0.32);
    ctx.fill();
    ctx.stroke();
    ctx.fillStyle = "#fffdf7";
    ctx.font = `900 ${Math.max(7, s * 0.5)}px ui-sans-serif`;
    ctx.textAlign = "center";
    ctx.textBaseline = "middle";
    ctx.fillText(agent.name, cx, tagY + s * 0.58, tagW - s * 0.4);
  }
  if (isPlayer) {
    ctx.fillStyle = "#151515";
    ctx.font = `900 ${Math.max(10, s * 0.9)}px ui-sans-serif`;
    ctx.textAlign = "center";
    ctx.fillText("JI", cx, cy + s * 3);
  }
  ctx.restore();
}

function drawTouchCue() {
  if (state.mode !== "playing" || !state.touchCue) return;
  const cue = state.touchCue;
  const alpha = clamp(cue.life / cue.maxLife, 0, 1);
  const vectors = {
    up: [0, -1],
    down: [0, 1],
    left: [-1, 0],
    right: [1, 0]
  };
  const [vx, vy] = vectors[cue.dir] || [0, -1];
  const radius = 18 + (1 - alpha) * 18;

  ctx.save();
  ctx.globalAlpha = alpha;
  ctx.translate(cue.x, cue.y);
  ctx.strokeStyle = "rgba(21, 21, 21, 0.72)";
  ctx.fillStyle = "rgba(255, 253, 247, 0.78)";
  ctx.lineWidth = 2;
  ctx.beginPath();
  ctx.arc(0, 0, radius, 0, Math.PI * 2);
  ctx.fill();
  ctx.stroke();

  ctx.fillStyle = state.party.color;
  ctx.strokeStyle = "#151515";
  ctx.lineWidth = 2.5;
  ctx.beginPath();
  ctx.moveTo(vx * 20, vy * 20);
  ctx.lineTo(vx * -8 + vy * 8, vy * -8 - vx * 8);
  ctx.lineTo(vx * -8 - vy * 8, vy * -8 + vx * 8);
  ctx.closePath();
  ctx.fill();
  ctx.stroke();
  ctx.restore();
  ctx.globalAlpha = 1;
}

function drawLeaderStatue() {
  const s = state.cellSize;
  const x = state.offsetX + 31 * s;
  const y = state.offsetY + 19 * s;
  ctx.save();
  drawMiniPerson(x, y, s * 2.8, {
    color: "#fffdf7",
    accent: state.party.color,
    symbol: state.party.symbol,
    foldedHands: true,
    phase: 0.2
  });
  ctx.fillStyle = "#151515";
  ctx.font = `900 ${Math.max(8, s * 0.62)}px ui-sans-serif`;
  ctx.textAlign = "center";
  ctx.fillText("CM FACE", x, y + s * 3.6);
  ctx.restore();
}

function draw() {
  const width = state.canvasWidth || canvas.clientWidth || 1;
  const height = state.canvasHeight || canvas.clientHeight || 1;
  if (document.body.dataset.mode !== state.mode) {
    document.body.dataset.mode = state.mode;
  }
  if (state.mode === "map" || state.mode === "confirm") {
    drawMapHome(width, height);
    return;
  }
  drawGridBackground(width, height);
  drawRegionArena();
  drawCampaignRoads();
  drawTerritory();
  drawCampaignProps();
  drawEventScenes();
  drawAmbientPeople();
  drawClaimBursts();
  drawTrails();
  drawTrailPaths();
  drawConversionBursts();
  for (const opponent of state.opponents) drawAgent(opponent, false);
  drawSupporters();
  if (state.player) drawAgent(state.player, true);
  drawReadyPulse();
  drawDangerCue();
  drawTouchCue();
  drawNetaHud(width);
  drawPauseOverlay(width, height);
}

function loop(timestamp) {
  const dt = Math.min(0.05, (timestamp - state.lastTime) / 1000 || 0);
  state.lastTime = timestamp;
  updateScreenShake(dt);
  update(dt);
  draw();
  requestAnimationFrame(loop);
}

function canvasPointFromEvent(event) {
  const rect = canvas.getBoundingClientRect();
  return {
    x: event.clientX - rect.left,
    y: event.clientY - rect.top
  };
}

function setTouchCue(point, dir) {
  state.touchCue = {
    x: point.x,
    y: point.y,
    dir,
    life: 0.34,
    maxLife: 0.34
  };
}

function updateTouchCue(dt) {
  if (!state.touchCue) return;
  state.touchCue.life -= dt;
  if (state.touchCue.life <= 0) state.touchCue = null;
}

function pickRegionFromMap(point) {
  let polygonHit = null;
  let polygonHitArea = Infinity;
  for (const region of REGIONS) {
    if (!pointInRegionMapShape(point, region)) continue;
    const area = getContainingRegionPolygonArea(point, region);
    if (area < polygonHitArea) {
      polygonHit = region;
      polygonHitArea = area;
    }
  }
  if (polygonHit) return polygonHit;

  let best = null;
  let bestScore = Infinity;
  for (const region of REGIONS) {
    const flag = getRegionMapPoint(region);
    const [rxNorm, ryNorm] = getRegionBlobSize(region);
    const profile = getRegionShapeProfile(region);
    const rx = Math.max(15, state.mapRect.width * rxNorm * profile.scaleX * 1.32);
    const ry = Math.max(15, state.mapRect.height * ryNorm * profile.scaleY * 1.32);
    const dx = (point.x - flag.x) / rx;
    const dy = (point.y - flag.y) / ry;
    const score = Math.hypot(dx, dy);
    const fingerRadius = region.type === "UT" ? 1.35 : 1.05;
    if (score < fingerRadius && score < bestScore) {
      best = region;
      bestScore = score;
    }
  }
  return best;
}

function handleMapPointer(event) {
  const region = pickRegionFromMap(canvasPointFromEvent(event));
  if (region) {
    showRegionPrompt(region.id);
  } else {
    confirmPanel.hidden = true;
    state.campaign.pendingRegionId = null;
  }
}

function pointerToDirection(event) {
  if (state.mode === "map" || state.mode === "confirm") {
    handleMapPointer(event);
    return;
  }
  if (!state.player || state.mode !== "playing") return;
  const point = canvasPointFromEvent(event);
  const px = (point.x - state.offsetX) / state.cellSize;
  const py = (point.y - state.offsetY) / state.cellSize;
  const dx = px - state.player.x;
  const dy = py - state.player.y;
  let dir;
  if (Math.abs(dx) > Math.abs(dy)) {
    dir = dx > 0 ? "right" : "left";
  } else {
    dir = dy > 0 ? "down" : "up";
  }
  setDirection(state.player, dir);
  setTouchCue(point, dir);
}

function useDecision(type) {
  const decision = DECISIONS[type];
  if (!decision || state.mode !== "playing") return;
  if (!state.roundStarted) {
    showToast("Campaign yatra abhi ready hai.");
    return;
  }
  if (state.neta.decisionClock > 0) {
    showToast("Campaign team is still moving.");
    return;
  }
  if (!canAfford(decision.cost)) {
    showToast("Neta meter low for this decision.");
    return;
  }
  ensureAudio();
  const costDelta = {};
  for (const [key, value] of Object.entries(decision.cost || {})) {
    costDelta[key] = -value;
  }
  adjustNeta(combineResourceChanges(costDelta, decision.impact));
  state.neta.decisionClock = 3.8;
  if (decision.claimRadius && state.player) {
    const cells = claimDisk(1, state.player.x, state.player.y, decision.claimRadius);
    addClaimBurst(cells, state.party.color);
    updateStats();
  }
  addFeed(`${decision.label}: support ${state.neta.support}, funds ${state.neta.funds}, rep ${state.neta.reputation}.`);
  showToast(decision.toast);
  playSound(type === "donorLunch" ? "event" : "rally");
  saveCampaignProgress();
  updateNetaPanel();
}

function bindEvents() {
  window.addEventListener("resize", resizeCanvas);
  document.addEventListener("visibilitychange", () => {
    if (document.hidden) autoPauseCampaign();
  });
  window.addEventListener("pagehide", autoPauseCampaign);
  window.addEventListener("keydown", (event) => {
    state.keys.add(event.key);
    if (["ArrowUp", "ArrowDown", "ArrowLeft", "ArrowRight", " "].includes(event.key)) {
      event.preventDefault();
    }
    if (event.key === " ") useRallyBoost();
    if (event.key.toLowerCase() === "p") togglePause();
  });
  window.addEventListener("keyup", (event) => state.keys.delete(event.key));
  canvas.addEventListener("pointerdown", (event) => {
    event.preventDefault();
    canvas.setPointerCapture?.(event.pointerId);
    ensureAudio();
    if (state.mode === "playing") triggerHaptic(5);
    const point = canvasPointFromEvent(event);
    state.pointer = { x: point.x, y: point.y, active: true, lastDir: null };
    pointerToDirection(event);
  });
  canvas.addEventListener("pointermove", (event) => {
    event.preventDefault();
    if (!state.pointer.active || state.mode !== "playing") return;
    const point = canvasPointFromEvent(event);
    const dx = point.x - state.pointer.x;
    const dy = point.y - state.pointer.y;
    const swipeThreshold = clamp(state.cellSize * 0.82, 10, 22);
    if (Math.hypot(dx, dy) > swipeThreshold) {
      const dir = Math.abs(dx) > Math.abs(dy) ? (dx > 0 ? "right" : "left") : dy > 0 ? "down" : "up";
      if (dir !== state.pointer.lastDir) {
        setDirection(state.player, dir);
        setTouchCue(point, dir);
      }
      state.pointer = { x: point.x, y: point.y, active: true, lastDir: dir };
    }
  });
  canvas.addEventListener("pointerup", (event) => {
    canvas.releasePointerCapture?.(event.pointerId);
    state.pointer.active = false;
  });
  canvas.addEventListener("pointercancel", (event) => {
    canvas.releasePointerCapture?.(event.pointerId);
    state.pointer.active = false;
  });
  document.querySelectorAll(".dir-btn").forEach((button) => {
    button.addEventListener("click", () => setDirection(state.player, button.dataset.dir));
  });
  decisionButtons.forEach((button) => {
    button.addEventListener("click", () => useDecision(button.dataset.decision));
  });
  boostBtn?.addEventListener("click", useRallyBoost);
  pauseBtn?.addEventListener("click", togglePause);
  startBtn?.addEventListener("click", startGame);
  quickDemoBtn?.addEventListener("click", () => activateQuickDemo());
  pitchBtn?.addEventListener("click", openPitchCard);
  pitchBtnSetup?.addEventListener("click", openPitchCard);
  closePitchBtn?.addEventListener("click", closePitchCard);
  onboardingNextBtn?.addEventListener("click", nextOnboardingStep);
  onboardingSkipBtn?.addEventListener("click", () => hideOnboarding());
  restartBtn?.addEventListener("click", () => {
    resultModal.classList.remove("is-open");
    if (getActiveRegion()) {
      selectRegion(state.campaign.activeRegionId);
    } else {
      setupModal.classList.add("is-open");
      state.mode = "setup";
    }
  });
  nextRegionBtn?.addEventListener("click", openRegionModal);
  openMapBtn?.addEventListener("click", openRegionModal);
  installBtn?.addEventListener("click", handleInstallClick);
  resetProgressBtn?.addEventListener("click", handleResetProgressClick);
  confirmRegionBtn?.addEventListener("click", confirmPendingRegion);
  cancelRegionBtn?.addEventListener("click", () => {
    state.campaign.pendingRegionId = null;
    confirmPanel.hidden = true;
    state.mode = "map";
    updateMobileHint();
  });
  shareBtn?.addEventListener("click", shareResult);
  posterBtn?.addEventListener("click", saveResultPoster);
  partyNameInput?.addEventListener("input", () => {
    nameError.textContent = validatePartyName(partyNameInput.value.trim()) || validateSlogan(sloganInput.value.trim());
  });
  sloganInput?.addEventListener("input", () => {
    nameError.textContent = validatePartyName(partyNameInput.value.trim()) || validateSlogan(sloganInput.value.trim());
  });
}

function setPaused(paused, message = "") {
  if (state.mode !== "playing" || !state.roundStarted) return false;
  state.paused = paused;
  pauseBtn.textContent = state.paused ? "Resume" : "Pause";
  eventStat.textContent = state.paused ? "Paused" : "Campaign";
  if (message) showToast(message);
  updateNetaPanel();
  return true;
}

function togglePause() {
  if (state.mode !== "playing" || !state.roundStarted) {
    showToast("Campaign yatra abhi ready hai.");
    return;
  }
  ensureAudio();
  const paused = !state.paused;
  setPaused(paused, paused ? "Campaign paused." : "Campaign resumed.");
  playSound("tap");
}

function autoPauseCampaign() {
  if (!state.paused && setPaused(true, "Campaign auto-paused.")) {
    addFeed("Campaign auto-paused while the app was away.");
  }
}

function useRallyBoost() {
  if (state.mode !== "playing" || state.paused || state.boostClock > 0) return;
  ensureAudio();
  state.speedMul = 1.42;
  state.boostClock = 2.2;
  showToast("Rally sprint activated.");
  playSound("rally");
  triggerHaptic([8, 22, 8]);
}

function isStandaloneDisplay() {
  return window.matchMedia?.("(display-mode: standalone)")?.matches || window.navigator.standalone === true;
}

function updateInstallButton() {
  if (!installBtn) return;
  const installed = state.appInstalled || isStandaloneDisplay();
  installBtn.hidden = installed;
  installBtn.textContent = state.installPromptEvent ? "Install" : "Install";
  installBtn.title = state.installPromptEvent ? "Install NETA JI" : "Use browser menu if install prompt is unavailable";
}

async function handleInstallClick() {
  ensureAudio();
  if (state.appInstalled || isStandaloneDisplay()) {
    showToast("NETA JI is already running like an app.");
    updateInstallButton();
    return;
  }
  if (!state.installPromptEvent) {
    showToast("Phone browser menu se Add to Home Screen choose karo.");
    addFeed("Install tip: browser menu me Add to Home Screen / Install App use karo.");
    triggerHaptic(8);
    return;
  }

  const promptEvent = state.installPromptEvent;
  state.installPromptEvent = null;
  updateInstallButton();
  try {
    promptEvent.prompt();
    const choice = await promptEvent.userChoice;
    if (choice?.outcome === "accepted") {
      showToast("NETA JI install started.");
      addFeed("NETA JI install prompt accepted.");
    } else {
      showToast("Install cancelled. You can try again later.");
    }
  } catch {
    showToast("Install prompt unavailable. Use browser menu.");
  } finally {
    updateInstallButton();
  }
}

function bindInstallEvents() {
  updateInstallButton();
  window.addEventListener("beforeinstallprompt", (event) => {
    event.preventDefault();
    state.installPromptEvent = event;
    updateInstallButton();
    addFeed("Install button ready for mobile app mode.");
  });
  window.addEventListener("appinstalled", () => {
    state.appInstalled = true;
    state.installPromptEvent = null;
    updateInstallButton();
    showToast("NETA JI installed.");
  });
  window.matchMedia?.("(display-mode: standalone)")?.addEventListener?.("change", updateInstallButton);
}

function registerServiceWorker() {
  if (!("serviceWorker" in navigator)) return;
  navigator.serviceWorker.register("sw.js").catch(() => {});
}

function registerDebugSnapshot() {
  window.__NETA_JI_DEBUG__ = () => ({
    mode: state.mode,
    activeRegion: getActiveRegion()?.name || null,
    pendingRegionId: state.campaign.pendingRegionId,
    demoMode: state.demoMode,
    influence: state.influence,
    mandateScore: mandateScore(),
    completedRegions: completedRegionCount(),
    nationalWon: state.campaign.nationalWon,
    roundStarted: state.roundStarted,
    paused: state.paused,
    timeLeft: Math.ceil(state.timeLeft),
    support: state.neta.support,
    funds: state.neta.funds,
    power: state.neta.power,
    reputation: state.neta.reputation,
    opponents: state.opponents.length,
    supporters: state.supporters.length,
    eventScenes: state.eventScenes.length,
    dangerCue: Boolean(state.dangerCue),
    installReady: Boolean(state.installPromptEvent),
    standalone: isStandaloneDisplay(),
    dpr: state.dpr,
    canvas: `${state.canvasWidth}x${state.canvasHeight}`
  });
}

function wrapPosterText(posterCtx, text, x, y, maxWidth, lineHeight, maxLines = 4) {
  const words = String(text || "").split(/\s+/).filter(Boolean);
  const lines = [];
  let line = "";
  for (const word of words) {
    const testLine = line ? `${line} ${word}` : word;
    if (posterCtx.measureText(testLine).width > maxWidth && line) {
      lines.push(line);
      line = word;
      if (lines.length === maxLines) break;
    } else {
      line = testLine;
    }
  }
  if (line && lines.length < maxLines) lines.push(line);
  lines.forEach((item, i) => posterCtx.fillText(item, x, y + i * lineHeight, maxWidth));
  return y + lines.length * lineHeight;
}

function drawPosterLeader(posterCtx, x, y, size, color, symbol) {
  posterCtx.save();
  posterCtx.translate(x, y);
  posterCtx.fillStyle = "rgba(21, 21, 21, 0.18)";
  posterCtx.beginPath();
  posterCtx.ellipse(0, size * 1.18, size * 0.95, size * 0.24, 0, 0, Math.PI * 2);
  posterCtx.fill();

  posterCtx.strokeStyle = "#151515";
  posterCtx.lineWidth = Math.max(5, size * 0.08);
  posterCtx.lineJoin = "round";
  posterCtx.lineCap = "round";
  posterCtx.fillStyle = "#f0b37e";
  posterCtx.beginPath();
  posterCtx.arc(0, -size * 0.35, size * 0.34, 0, Math.PI * 2);
  posterCtx.fill();
  posterCtx.stroke();
  posterCtx.fillStyle = "#151515";
  posterCtx.beginPath();
  posterCtx.arc(0, -size * 0.46, size * 0.3, Math.PI, Math.PI * 2);
  posterCtx.fill();

  posterCtx.fillStyle = "#fffdf7";
  posterCtx.beginPath();
  posterCtx.roundRect(-size * 0.5, -size * 0.02, size, size * 0.98, size * 0.12);
  posterCtx.fill();
  posterCtx.stroke();
  posterCtx.fillStyle = color;
  posterCtx.fillRect(-size * 0.06, -size * 0.02, size * 0.12, size * 0.98);

  posterCtx.strokeStyle = "#151515";
  posterCtx.beginPath();
  posterCtx.moveTo(-size * 0.48, size * 0.18);
  posterCtx.lineTo(-size * 0.08, size * 0.46);
  posterCtx.moveTo(size * 0.48, size * 0.18);
  posterCtx.lineTo(size * 0.08, size * 0.46);
  posterCtx.stroke();
  posterCtx.fillStyle = "#f0b37e";
  posterCtx.beginPath();
  posterCtx.ellipse(-size * 0.05, size * 0.48, size * 0.12, size * 0.18, -0.4, 0, Math.PI * 2);
  posterCtx.ellipse(size * 0.05, size * 0.48, size * 0.12, size * 0.18, 0.4, 0, Math.PI * 2);
  posterCtx.fill();
  posterCtx.stroke();

  posterCtx.fillStyle = color;
  posterCtx.strokeStyle = "#151515";
  posterCtx.beginPath();
  posterCtx.roundRect(-size * 0.32, size * 0.62, size * 0.64, size * 0.28, size * 0.14);
  posterCtx.fill();
  posterCtx.stroke();
  posterCtx.fillStyle = "#fffdf7";
  posterCtx.font = `900 ${Math.max(18, size * 0.16)}px ui-sans-serif`;
  posterCtx.textAlign = "center";
  posterCtx.textBaseline = "middle";
  posterCtx.fillText(symbol || "NETA", 0, size * 0.76, size * 0.52);
  posterCtx.restore();
}

function canvasToBlob(canvas) {
  return new Promise((resolve) => canvas.toBlob(resolve, "image/png", 0.94));
}

async function saveResultPoster() {
  const summary = state.resultSummary || {
    headline: resultHeadline.textContent,
    copy: resultCopy.textContent,
    stamp: resultStamp.textContent,
    victory: resultModal.dataset.result !== "loss",
    nationalComplete: resultModal.dataset.result === "national",
    regionName: getActiveRegion()?.name || "NETA JI",
    score: mandateScore(),
    influence: state.influence,
    support: state.neta.support,
    reputation: state.neta.reputation,
    completedRegions: completedRegionCount(),
    partyName: state.party.name,
    slogan: state.party.slogan,
    color: state.party.color,
    symbol: symbolLabel(state.party.symbol)
  };

  try {
    ensureAudio();
    if (posterBtn) {
      posterBtn.disabled = true;
      posterBtn.textContent = "Making...";
    }
    const posterCanvas = document.createElement("canvas");
    posterCanvas.width = 1080;
    posterCanvas.height = 1350;
    const posterCtx = posterCanvas.getContext("2d");
    const partyColor = summary.color || state.party.color;

    const bg = posterCtx.createLinearGradient(0, 0, 1080, 1350);
    bg.addColorStop(0, "#fff3d6");
    bg.addColorStop(0.48, "#fffdf7");
    bg.addColorStop(1, "#bfe7ff");
    posterCtx.fillStyle = bg;
    posterCtx.fillRect(0, 0, 1080, 1350);

    posterCtx.fillStyle = partyColor;
    posterCtx.globalAlpha = 0.18;
    for (let x = -80; x < 1160; x += 110) {
      posterCtx.fillRect(x, 0, 34, 1350);
    }
    posterCtx.globalAlpha = 1;

    posterCtx.strokeStyle = "#151515";
    posterCtx.lineWidth = 10;
    posterCtx.strokeRect(48, 48, 984, 1254);
    posterCtx.setLineDash([24, 18]);
    posterCtx.lineWidth = 5;
    posterCtx.strokeStyle = partyColor;
    posterCtx.strokeRect(76, 76, 928, 1198);
    posterCtx.setLineDash([]);

    posterCtx.fillStyle = partyColor;
    posterCtx.strokeStyle = "#151515";
    posterCtx.lineWidth = 7;
    posterCtx.beginPath();
    posterCtx.roundRect(720, 92, 230, 76, 38);
    posterCtx.fill();
    posterCtx.stroke();
    posterCtx.fillStyle = "#fffdf7";
    posterCtx.font = "900 38px ui-sans-serif";
    posterCtx.textAlign = "center";
    posterCtx.textBaseline = "middle";
    posterCtx.fillText(String(summary.stamp || "Mandate").toUpperCase(), 835, 130, 190);

    posterCtx.fillStyle = "#151515";
    posterCtx.font = "900 44px ui-sans-serif";
    posterCtx.textAlign = "left";
    posterCtx.fillText("BREAKING POSTER NEWS", 100, 146);
    posterCtx.font = "900 78px ui-sans-serif";
    const headlineBottom = wrapPosterText(posterCtx, summary.headline, 100, 260, 880, 86, 3);

    drawPosterLeader(posterCtx, 540, Math.max(520, headlineBottom + 150), 220, partyColor, summary.symbol);

    posterCtx.fillStyle = "#151515";
    posterCtx.font = "900 42px ui-sans-serif";
    posterCtx.textAlign = "center";
    posterCtx.fillText(summary.partyName, 540, 830, 860);
    posterCtx.fillStyle = "#625a4e";
    posterCtx.font = "800 30px ui-sans-serif";
    posterCtx.fillText(summary.slogan, 540, 878, 820);

    posterCtx.fillStyle = "#fffdf7";
    posterCtx.strokeStyle = "#151515";
    posterCtx.lineWidth = 6;
    posterCtx.beginPath();
    posterCtx.roundRect(100, 930, 880, 170, 22);
    posterCtx.fill();
    posterCtx.stroke();
    posterCtx.fillStyle = "#151515";
    posterCtx.font = "900 34px ui-sans-serif";
    posterCtx.textAlign = "left";
    wrapPosterText(posterCtx, summary.copy, 132, 985, 816, 42, 3);

    const stats = [
      ["MANDATE", String(summary.score)],
      ["INFLUENCE", `${summary.influence}%`],
      ["SUPPORT", String(summary.support)],
      ["REP", String(summary.reputation)]
    ];
    stats.forEach(([label, value], i) => {
      const x = 100 + i * 225;
      posterCtx.fillStyle = i % 2 ? "#fffdf7" : partyColor;
      posterCtx.strokeStyle = "#151515";
      posterCtx.lineWidth = 5;
      posterCtx.beginPath();
      posterCtx.roundRect(x, 1140, 190, 96, 16);
      posterCtx.fill();
      posterCtx.stroke();
      posterCtx.fillStyle = i % 2 ? "#151515" : "#fffdf7";
      posterCtx.font = "900 34px ui-sans-serif";
      posterCtx.textAlign = "center";
      posterCtx.fillText(value, x + 95, 1176, 150);
      posterCtx.font = "900 18px ui-sans-serif";
      posterCtx.fillText(label, x + 95, 1210, 150);
    });

    posterCtx.fillStyle = "#151515";
    posterCtx.font = "900 26px ui-sans-serif";
    posterCtx.textAlign = "center";
    posterCtx.fillText("NETA JI - fictional comedy election arcade", 540, 1280, 840);

    const blob = await canvasToBlob(posterCanvas);
    if (!blob) throw new Error("Poster export failed");
    const filename = `neta-ji-${String(summary.regionName || "result").toLowerCase().replace(/[^a-z0-9]+/g, "-").replace(/^-|-$/g, "") || "result"}.png`;

    if (typeof File !== "undefined") {
      const file = new File([blob], filename, { type: "image/png" });
      if (navigator.canShare?.({ files: [file] }) && navigator.share) {
        await navigator.share({ title: "NETA JI", text: state.shareText, files: [file] });
        showToast("Poster shared.");
        playSound("tap");
        triggerHaptic(10);
        return;
      }
    }

    const url = URL.createObjectURL(blob);
    const link = document.createElement("a");
    link.href = url;
    link.download = filename;
    document.body.append(link);
    link.click();
    link.remove();
    window.setTimeout(() => URL.revokeObjectURL(url), 1200);
    showToast("Poster PNG saved.");
    playSound("tap");
    triggerHaptic(10);
  } catch {
    showToast("Poster save cancelled.");
  } finally {
    if (posterBtn) {
      posterBtn.disabled = false;
      posterBtn.textContent = "Save Poster";
    }
  }
}

async function shareResult() {
  const text = state.shareText || `NETA JI: ${state.party.name} is ready for a comedy mandate.`;
  try {
    if (navigator.share) {
      await navigator.share({ title: "NETA JI", text });
    } else if (navigator.clipboard) {
      await navigator.clipboard.writeText(text);
      showToast("Fictional share text copied.");
    }
  } catch {
    showToast("Share cancelled.");
  }
}

async function init() {
  await loadGameData();
  loadCampaignProgress();
  setPartyPreview();
  renderRegionHub();
  bindEvents();
  resizeCanvas();
  resetGame();
  registerDebugSnapshot();
  bindInstallEvents();
  registerServiceWorker();
  if (DEMO_FROM_QUERY) {
    activateQuickDemo({ fromUrl: true });
  }
  requestAnimationFrame(loop);
}

init();
