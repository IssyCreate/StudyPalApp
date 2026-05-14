-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 14, 2026 at 10:17 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `studypal`
--

-- --------------------------------------------------------

--
-- Table structure for table `assignments`
--

CREATE TABLE `assignments` (
  `id` int(11) NOT NULL,
  `title` varchar(255) DEFAULT NULL,
  `subject` varchar(100) DEFAULT NULL,
  `instructions` varchar(255) NOT NULL,
  `due_date` datetime DEFAULT NULL,
  `due_time` time NOT NULL,
  `user_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `assignments`
--

INSERT INTO `assignments` (`id`, `title`, `subject`, `instructions`, `due_date`, `due_time`, `user_id`) VALUES
(11, 'History Essay', 'History', 'write a 200 word summary on colonization', '2026-05-14 00:00:00', '00:00:00', 1),
(12, 'Test', 'Test', 'Test', '2026-05-14 00:00:00', '13:00:00', 5),
(13, 'Test', 'test', 'test', '2026-05-14 00:00:00', '14:15:00', 6);

-- --------------------------------------------------------

--
-- Table structure for table `notes`
--

CREATE TABLE `notes` (
  `id` int(11) NOT NULL,
  `user_id` int(11) DEFAULT NULL,
  `subject` varchar(100) DEFAULT NULL,
  `content` text DEFAULT NULL,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `notes`
--

INSERT INTO `notes` (`id`, `user_id`, `subject`, `content`, `created_at`) VALUES
(2, 5, 'IT Test', 'Test at 1pm', '2026-05-14 17:52:10'),
(3, 6, 'Spanish', 'Study pronouns', '2026-05-14 19:15:41'),
(4, 6, 'Spanish test', 'test', '2026-05-14 19:19:18');

-- --------------------------------------------------------

--
-- Table structure for table `quizzes`
--

CREATE TABLE `quizzes` (
  `id` int(11) NOT NULL,
  `question` text DEFAULT NULL,
  `option1` varchar(255) DEFAULT NULL,
  `option2` varchar(255) DEFAULT NULL,
  `option3` varchar(255) DEFAULT NULL,
  `option4` varchar(255) DEFAULT NULL,
  `correct_answer` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `quizzes`
--

INSERT INTO `quizzes` (`id`, `question`, `option1`, `option2`, `option3`, `option4`, `correct_answer`) VALUES
(1, 'What is the capital of Jamaica?', 'Kingston', 'Montego Bay', 'Spanish Town', 'Mandeville', 'Kingston'),
(2, 'Which planet is known as the Red Planet?', 'Mars', 'Venus', 'Jupiter', 'Saturn', 'Mars'),
(3, 'What does HTML stand for?', 'Hyper Text Markup Language', 'HighText Machine Language', 'Hyper Transfer Markup Language', 'Home Tool Markup Language', 'Hyper Text Markup Language');

-- --------------------------------------------------------

--
-- Table structure for table `quiz_results`
--

CREATE TABLE `quiz_results` (
  `id` int(11) NOT NULL,
  `user_id` int(11) DEFAULT NULL,
  `score` int(11) DEFAULT NULL,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `quiz_results`
--

INSERT INTO `quiz_results` (`id`, `user_id`, `score`, `created_at`) VALUES
(2, 3, 3, '2026-05-11 03:57:49'),
(3, 4, 3, '2026-05-12 01:17:27'),
(4, 4, 2, '2026-05-12 01:19:08'),
(5, 4, 3, '2026-05-12 01:19:18'),
(6, 3, 3, '2026-05-12 02:18:15'),
(7, 5, 3, '2026-05-12 02:34:05'),
(8, 5, 3, '2026-05-14 05:42:17'),
(9, 5, 3, '2026-05-14 17:32:33');

-- --------------------------------------------------------

--
-- Table structure for table `study_sessions`
--

CREATE TABLE `study_sessions` (
  `id` int(11) NOT NULL,
  `user_id` int(11) DEFAULT NULL,
  `subject` varchar(100) DEFAULT NULL,
  `duration_minutes` int(11) DEFAULT NULL,
  `completed_at` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `study_sessions`
--

INSERT INTO `study_sessions` (`id`, `user_id`, `subject`, `duration_minutes`, `completed_at`) VALUES
(2, 5, 'Quiz', 30, '2026-05-11 21:34:05'),
(3, 5, 'Calculus', 45, '2026-05-12 10:41:40'),
(4, 5, 'Math', 60, '2026-05-12 10:46:01'),
(5, 5, 'Spanish', 5, '2026-05-12 19:26:13'),
(6, 5, 'Math', 4, '2026-05-14 00:40:12'),
(7, 5, 'Quiz', 30, '2026-05-14 00:42:18'),
(8, 5, 'Quiz', 30, '2026-05-14 12:32:33'),
(9, 6, 'Quiz', 30, '2026-05-14 14:11:16'),
(10, 6, 'Spanish', 5, '2026-05-14 14:19:21');

-- --------------------------------------------------------

--
-- Table structure for table `timetable`
--

CREATE TABLE `timetable` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `Subject` varchar(100) NOT NULL,
  `Time` varchar(20) NOT NULL,
  `DurationMinutes` int(11) NOT NULL,
  `Day` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `timetable`
--

INSERT INTO `timetable` (`id`, `user_id`, `Subject`, `Time`, `DurationMinutes`, `Day`) VALUES
(1, 5, 'Spanish', '7:42', 5, 'Mon'),
(2, 5, 'Chinese', '8:00', 10, 'Wed'),
(3, 5, 'Spanish', '8:08', 10, 'Tue'),
(4, 5, 'History', '8:15', 10, 'Mon'),
(5, 5, 'Math', '8:30', 4, 'Mon'),
(6, 5, 'Chemistry', '5:00', 45, 'Thu'),
(7, 5, 'Geography', '12:05', 5, 'Thu'),
(8, 6, 'Spanish', '2:11', 5, 'Thu');

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `UserID` int(11) NOT NULL,
  `name` varchar(250) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `age` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`UserID`, `name`, `email`, `password`, `age`) VALUES
(1, 'James Bond', 'jbond@gmail.com', '$2y$10$x6Rk7Nsq1FwdXsURn5SZUu4yvD/r7YY/sTEbT2NeZ.uqKdYy5qoJm', 16),
(2, '', '', '$2y$10$KA5C0p5sD7uF65t3SNeyJuSwH.iXlXnMAF1A37tCGT.hBzq.3O4i6', 0),
(3, 'Jessie James', 'deja@gmail.com', '$2y$10$AeYGJVNQrk6hkDJhI479IOFxd/Y33zmn6mEqOTnzPsfjJU/ikgKQK', 19),
(4, 'Rachel Mark', 'rach@gmail.com', '$2y$10$CGQmtAvBvqMLVQGVpCuJmeF1szy0fs0Gd8453EKK.N27wvxK1pGum', 13),
(5, 'Joel Watkins', 'joejoe@gmail.com', '$2y$10$QYOjGyk2bq/Ktz2NDPvgJOk9RE1Ib4mKNO7wLZqe.VLoP8w.NTbse', 17),
(6, 'pATRICIA', 'pat@gmail.com', '$2y$10$H2N39bJBb7OwhEtn/wNbTOhHF3/XIscDeQrNqbaeK6rTuAOjOI8SG', 22);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `assignments`
--
ALTER TABLE `assignments`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `notes`
--
ALTER TABLE `notes`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `quizzes`
--
ALTER TABLE `quizzes`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `quiz_results`
--
ALTER TABLE `quiz_results`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `study_sessions`
--
ALTER TABLE `study_sessions`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `timetable`
--
ALTER TABLE `timetable`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_timetable_user` (`user_id`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`UserID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `assignments`
--
ALTER TABLE `assignments`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT for table `notes`
--
ALTER TABLE `notes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `quizzes`
--
ALTER TABLE `quizzes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `quiz_results`
--
ALTER TABLE `quiz_results`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `study_sessions`
--
ALTER TABLE `study_sessions`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `timetable`
--
ALTER TABLE `timetable`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `UserID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `assignments`
--
ALTER TABLE `assignments`
  ADD CONSTRAINT `assignments_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`UserID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `notes`
--
ALTER TABLE `notes`
  ADD CONSTRAINT `notes_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`UserID`) ON DELETE CASCADE;

--
-- Constraints for table `quiz_results`
--
ALTER TABLE `quiz_results`
  ADD CONSTRAINT `quiz_results_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`UserID`) ON DELETE CASCADE;

--
-- Constraints for table `study_sessions`
--
ALTER TABLE `study_sessions`
  ADD CONSTRAINT `study_sessions_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`UserID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `timetable`
--
ALTER TABLE `timetable`
  ADD CONSTRAINT `fk_timetable_user` FOREIGN KEY (`user_id`) REFERENCES `user` (`UserID`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
