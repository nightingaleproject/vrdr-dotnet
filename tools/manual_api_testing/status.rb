#!/usr/bin/env ruby
# Look at all the files in the current directory and try to figur out what's up submission wise

require 'active_support'

files = Dir.glob('*.json')

files.select! { |f| f.match(/^[0-9]{4}[A-Z]{2}[0-9]{6}_/) }

counts = Hash.new(0)

groups = files.group_by { |n| n.split('_').first }

groups.sort.each do |identifier, files|
  status = {}
  status[:submission] = files.any? { |f| f.match(/^#{identifier}.*_submission.json/) || f.match(/^#{identifier}.*_update.json/) }
  status[:acknowledgement] = files.any? { |f| f.match(/^#{identifier}.*_submission_acknowledgement.json/) }
  status[:cause_coding] = files.any? { |f| f.match(/^#{identifier}.*_cause_of_death_coding.json/) }
  status[:demographics_coding] = files.any? { |f| f.match(/^#{identifier}.*_demographics_coding.json/) }
  # status[:cause_coding_acknowledgement] = files.any? { |f| f.match(/^#{identifier}.*_cause_of_death_coding_acknowledgement.json/) }
  error = files.any? { |f| f.match(/^#{identifier}.*_extraction_error.json/) }
  coding_status = files.any? { |f| f.match(/^#{identifier}.*_status.json/) }
  missing = status.select { |k, v| !v }.map { |k, v| k }
  (status.keys - missing).each do |present|
    counts[present.to_sym] += 1
  end
  if status.values.all? && !error
    puts "#{identifier}: complete (acknowledged and coded)"
    counts[:complete] += 1
  elsif status.values.all? && error
    puts "#{identifier}: ACKNOWLEDGEMENT AND ERROR PRESENT"
    counts[:unexpected] += 1
  elsif error
    puts "#{identifier}: complete (error response received)"
    counts[:error] += 1 
  elsif status.reject { |k| k == :cause_coding || k == :demographics_coding }.values.all? && coding_status
    if status[:demographics_coding]
      puts "#{identifier}: complete (status and demographic coding response received)"
    else
      puts "#{identifier}: missing demographics coding (status response received)"
    end
    counts[:status] += 1
  else
    puts "#{identifier}: missing #{missing.map { |k| k.to_s.gsub('_', ' ') }.join(', ')}"
  end
end

puts
puts "Totals"
puts "------"
counts.each do |k, v|
  puts "#{k.to_s.gsub('_', ' ')}: #{v}"
end
